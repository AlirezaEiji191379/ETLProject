namespace ETLProject.Common.Table
{
    public class Column
    {
        public string Name { get; set; }
        public ColumnType Type { get; set; }

        public Type GetCsharpType()
        {
            switch (this.Type)
            {
                case ColumnType.StringType:
                    return typeof(string);
                case ColumnType.Int32Type:
                    return typeof(int);
                case ColumnType.BooleanType:
                    return typeof(bool);
                case ColumnType.DoubleType:
                    return typeof(double);
                default:
                    throw new NotSupportedException();
            }
        }
    }
}
