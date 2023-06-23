using SqlKata;
using SqlKata.Compilers;
using SqlKata.Execution;

var query = new Query("public.Users").Select("Id,Name");

var compiler = new MySqlCompiler();
Console.WriteLine(compiler.Compile(query));

