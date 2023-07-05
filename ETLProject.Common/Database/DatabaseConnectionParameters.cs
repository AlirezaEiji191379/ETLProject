namespace ETLProject.Common.Database
{
    public class DatabaseConnectionParameters
    {
        public string Host { get; set; }
        public string Port { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string DatabaseName { get; set; }
        public string ConnectionName { get; set; }
        public string Schema { get; set; }
        public DataSourceType DataSourceType { get; set; }
        public Guid Id { get; set; }
    }
}
