using DeviceStatsT.Data;
using DeviceStatsT.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DeviceStatsT.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DevicesController : ControllerBase
    {
        private readonly StatsIngestRepository _repo;
        public DevicesController(StatsIngestRepository repo)
        {
            _repo = repo;
        }
        [HttpGet]
        public async Task<IActionResult> GetDevices()
        {
            
            var devices = await _repo.GetDevicesAsync();
            return Ok(devices);

        }
        [HttpGet("{id:guid}/stats")]

        public async Task<IActionResult> GetDeviceStats(Guid id)
        {

            var deviceStats = await _repo.GetStatsByDeviceIdAsync(id);
            return Ok(deviceStats);

        }
    }
}
