using ETLProject.Common.Common.DIManager;
using ETLProject.DataSource.QueryManager.Common.DIManager;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCommonServices();
builder.Services.AddDataSourceQueryServices();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
