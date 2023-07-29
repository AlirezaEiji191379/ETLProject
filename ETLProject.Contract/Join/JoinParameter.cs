using ETLProject.Contract.DbWriter;
using ETLProject.Contract.Join.Enums;

namespace ETLProject.Contract.Join;

public class JoinParameter
{
    public JoinType JoinType { get; set; }
    public bool UseLeftTableConnection { get; set; }
    public string LeftTableJoinColumnName { get; set; }
    public string RigthTableJoinColumnName { get; set; }
    public List<string> LeftTableSelectedColumnNames { get; set; }
    public List<string> RigthTableSelectedColumnNames { get; set; }
    public BulkConfiguration? BulkConfiguration { get; set; }
}