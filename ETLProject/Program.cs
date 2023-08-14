using ETLProject.DIManager;
using ETLProject.Pipeline;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApiServices();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("AllowAnyUrl");

app.UseAuthorization();

app.MapControllers();

app.Run();
