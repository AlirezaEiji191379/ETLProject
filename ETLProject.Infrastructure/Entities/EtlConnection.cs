using ETLProject.Common.Database;

namespace ETLProject.Infrastructure.Entities;

public class EtlConnection
{
    public string Host { get; set; }
    public string Port { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string ConnectionName { get; set; }
    public DataSourceType DataSourceType { get; set; }
    public Guid Id { get; set; }
}