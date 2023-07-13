using ETLProject.Common.Abstractions;
using ETLProject.Common.Table;
using ETLProject.Contract.DBReader;
using ETLProject.DataSource.QueryBusiness.DbReaderBusiness.Abstractions;

namespace ETLProject.DataSource.QueryBusiness.DbReaderBusiness;

public class DbTableReader : IDbTableReader
{
    private readonly IRandomStringGenerator _randomStringGenerator;

    public DbTableReader(IRandomStringGenerator randomStringGenerator)
    {
        _randomStringGenerator = randomStringGenerator;
    }
    
    public ETLTable ReadTable(DbReaderContract dbReaderContract)
    {
        return new ETLTable()
        {
            TableName = dbReaderContract.TableName,
            TableType = TableType.Permanent,
            AliasName = _randomStringGenerator.GenerateRandomString(8),
            DataSourceType = dbReaderContract.DataSourceType,
            DatabaseConnection = dbReaderContract.DatabaseConnectionParameters,
            TableSchema = dbReaderContract.Schema,
            Columns = dbReaderContract.SelectedColumns.Select(selectedColumn => new ETLColumn()
            {
                Name = selectedColumn.Name,
                ETLColumnType = new ETLColumnType()
                {
                    Length = selectedColumn.Length,
                    Precision = selectedColumn.Precision,
                    Type = selectedColumn.ColumnType
                },
            }).ToList()
        };
    }
}