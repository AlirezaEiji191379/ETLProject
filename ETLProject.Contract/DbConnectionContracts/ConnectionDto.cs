using ETLProject.Common.Database;

namespace ETLProject.Contract.DbConnectionContracts;

public class ConnectionDto
{
    public string Host { get; init; }
    public string Port { get; init; }
    public string Username { get; init; }
    public string Password { get; init; }
    public string? ConnectionName { get; set; }
    public string? DatabaseName { get; set; }
    public DataSourceType DataSourceType { get; init; }
}