using DeviceStats.Data;
using DeviceStatsT.Dtos;
using Dapper;

namespace DeviceStatsT.Data
{
    public class StatsIngestRepository
    {
        private readonly IDbConnectionFactory _factory;

        public StatsIngestRepository(IDbConnectionFactory factory)
        {
            _factory = factory;

        }
        private const string UpsertDeviceSql = @"
            insert into public.devices (id, name, version)
            values (@DeviceId, @Name, @Version)
            on conflict (id) do update
            set name = excluded.name,
                version = excluded.version;
            ";
        private const string InsertStatsSql = @"
            insert into public.device_stats (device_id, start_time, end_time)
            values (@DeviceId, @StartTime, @EndTime);
            ";
        private const string ReadDevicesSql = @"
            select id, name, version from public.devices";
        private const string ReadStatsByDeviceIdSql = @"
            select id, start_time as StartTime, end_time as EndTime
            from public.device_stats
            where device_id=@DeviceId
            order by start_time desc
            ";

        public async Task SaveAsync(DeviceStatsRequestDto dto)
        {

            using (var connection = _factory.Create())
            {
                await connection.OpenAsync();
                using var tx = connection.BeginTransaction();
                try
                {
                    var parameters = new
                    {
                        DeviceId = dto.Id,
                        dto.Name,
                        dto.Version,
                        dto.StartTime,
                        dto.EndTime
                    };

                    await connection.ExecuteAsync(UpsertDeviceSql, parameters, tx);
                    await connection.ExecuteAsync(InsertStatsSql, parameters, tx);


                    tx.Commit();
                }
                catch 
                {
                    tx.Rollback();
                    throw;
                }
                

            }

        }
        public async Task<IEnumerable<DeviceDto>> GetDevicesAsync()
        {
            using (var connection = _factory.Create())
            {
                await connection.OpenAsync();
                

                    var devices = await connection.QueryAsync<DeviceDto>(ReadDevicesSql);
                    return devices;
               
                
            }
        }
        public async Task<IEnumerable<DeviceStatDto>> GetStatsByDeviceIdAsync(Guid deviceId)
        {
            using (var connection = _factory.Create())
            {
                await connection.OpenAsync();


                var deviceStats = await connection.QueryAsync<DeviceStatDto>(ReadStatsByDeviceIdSql, new { DeviceId = deviceId });
                return deviceStats;


            }
        }
    }
}
