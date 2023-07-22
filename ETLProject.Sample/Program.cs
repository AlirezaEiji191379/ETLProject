
using ETLProject.Sample.ETLSamples;

Console.WriteLine();
try
{
    await DbAddSamples.AddSample();
}
catch (Exception e)
{
    Console.WriteLine(e);
    throw;
}

