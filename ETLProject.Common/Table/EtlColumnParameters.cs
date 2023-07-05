
namespace ETLProject.Common.Table
{
    public class EtlColumnParameters
    {
        public bool IsAutoIncrement { get; set; }
        public bool IsIdentity { get; set; }
        public bool IsPrimaryKey { get; set; }
        public bool IsNullable { get; set; } = true;
        public bool IsUnique { get; set; }
    }
}
