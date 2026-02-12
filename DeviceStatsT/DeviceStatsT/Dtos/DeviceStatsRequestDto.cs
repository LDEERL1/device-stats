using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DeviceStatsT.Dtos
{
    public class DeviceStatsRequestDto
    {
        [JsonPropertyName("_id")]
        public Guid Id { get; set; }
        [Required]
        [StringLength(256, MinimumLength = 3)]
        public string Name { get; set; }
        public DateTimeOffset StartTime { get; set; } 
        public DateTimeOffset EndTime { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 1)]
        public string Version { get; set; }
    }
}
