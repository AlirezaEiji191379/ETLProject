namespace ETLProject.Common.Table
{
    public class ETLColumn
    {
        public string Name { get; set; }
        public ETLColumnType ETLColumnType { get; set; }

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
                default:
                    throw new NotSupportedException();
            }
        }
    }
}
