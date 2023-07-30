using ETLProject.Contract.DbWriter;
using ETLProject.Contract.Join.Enums;

namespace ETLProject.Contract.Join;

public class JoinParameter
{
    public JoinType JoinType { get; set; }
    public bool UseLeftTableConnection { get; set; }
    public string LeftTableJoinColumnName { get; set; }
    public string RigthTableJoinColumnName { get; set; }
    public List<JoinColumnParameter> LeftTableSelectedColumns { get; set; }
    public List<JoinColumnParameter> RigthTableSelectedColumns { get; set; }
    public BulkConfiguration? BulkConfiguration { get; set; }
}