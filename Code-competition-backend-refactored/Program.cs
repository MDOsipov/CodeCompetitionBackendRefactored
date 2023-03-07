using Code_competition_backend_refactored.Extensions;
using Newtonsoft.Json;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.ConfigureCors();
builder.Services.ConfigureMsSqlContext(builder.Configuration);
builder.Services.ConfigureRepositoryWrapper();
builder.Services.ConfigureIISIntegration();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.ConfigureLogging();

builder.Services.AddControllers().AddNewtonsoftJson(options => {
	options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
});


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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
