public class DeviceStatDto
{
    public Guid StatId { get; set; }
    public Guid DeviceId { get; set; }
    public DateTimeOffset StartTime { get; set; }
    public DateTimeOffset EndTime { get; set; }
}
