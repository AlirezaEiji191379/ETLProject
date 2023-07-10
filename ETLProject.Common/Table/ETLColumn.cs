using Org.BouncyCastle.Asn1.X509.Qualified;

namespace ETLProject.Common.Table
{
    public class ETLColumn
    {
        public ETLColumn()
        {
            EtlColumnId = Guid.NewGuid();
            EtlColumnParameters = new EtlColumnParameters();
        }
        public Guid EtlColumnId { get; set; }
        public string Name { get; set; }
        public ETLColumnType ETLColumnType { get; set; }
        public EtlColumnParameters EtlColumnParameters { get; set; }

        public Type GetCsharpType()
        {
            switch (this.ETLColumnType.Type)
            {
                case ColumnType.StringType:
                    return typeof(string);
                case ColumnType.Int32Type:
                    return typeof(int);
                case ColumnType.BooleanType:
                    return typeof(bool);
                case ColumnType.DoubleType:
                    return typeof(double);
                case ColumnType.Guid:
                    return typeof(Guid);
                default:
                    throw new NotSupportedException();
            }
        }

        public ColumnType GetColumnTypeFromCSharp(Type type)
        {
            if (type == typeof(int))
                return ColumnType.Int32Type;
            if (type == typeof(string))
                return ColumnType.StringType;
            if(type == typeof(double))
                return ColumnType.DoubleType;
            if(type == typeof(bool))
                return ColumnType.BooleanType;
            if (type == typeof(Guid))
                return ColumnType.Guid;
            throw new NotSupportedException();
        }

        public ETLColumn Clone()
        {
            return new ETLColumn()
            {
                Name = Name,
                ETLColumnType = new ETLColumnType()
                {
                    Length = ETLColumnType.Length,
                    Precision = ETLColumnType.Precision,
                    Type = ETLColumnType.Type
                }
            };
        }

    }
}
