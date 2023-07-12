namespace ETLProject.Infrastructure;

public class MainDatabaseConnectionStringBuilder
{
    public static string GetPostgresqlConnection()
    {
        return "Host=localhost;Port=5432;Database=ETL_Main;Username=postgres;Password=92?VH2WMrx";
    }
}