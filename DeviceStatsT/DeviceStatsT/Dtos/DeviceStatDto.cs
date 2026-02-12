namespace DeviceStatsT.Dtos
{
    public class DeviceStatDto
    {
        public Guid Id { get; set; }
        public DateTimeOffset StartTime { get; set; }
        public DateTimeOffset EndTime { get; set; }
    }

}
