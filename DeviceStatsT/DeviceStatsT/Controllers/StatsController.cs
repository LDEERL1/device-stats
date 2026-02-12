

using DeviceStatsT.Data;
using DeviceStatsT.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;


namespace DeviceStatsT.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StatsController : ControllerBase
    {
        private readonly StatsIngestRepository _repo;
        public StatsController(StatsIngestRepository repo) 
        { 
            _repo = repo; 
        }

    [HttpPost]
        public async Task<IActionResult> Post(DeviceStatsRequestDto dto)
        {
            if (dto.EndTime <= dto.StartTime) 
                return BadRequest("EndTime must be greater than StartTime");
            

            if (!IsValidSemVer(dto.Version)) 
                return BadRequest("Version must be in format X.Y.Z");

            await _repo.SaveAsync(dto); 
            return Ok();

        }
        private bool IsValidSemVer(string version)
        {
            if (string.IsNullOrWhiteSpace(version))
                return false;

            string[] parts = version.Trim().Split('.');
            if (parts.Length != 3 && parts.Length != 4)
                return false;

            foreach (var part in parts)
            {
                if (!int.TryParse(part, out int n) || n < 0)
                    return false;
            }

            return true;
        }

    }
}
