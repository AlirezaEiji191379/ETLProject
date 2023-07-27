
using SqlKata;
using SqlKata.Compilers;

var query = new Query("Users").Select("id","fullname");
var insertQuery = new Query("Samples").AsInsert(new List<string>(){},query);

var compiler = new SqlServerCompiler();

Console.WriteLine(compiler.Compile(insertQuery));
