using Transferencias.Persistence;
using Microsoft.EntityFrameworkCore;
using Transferencias.Persistence.Repositories;
using Transferencias.App.Services;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Contexto de base de datos
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection"))
);

// AutoMapper
builder.Services.AddAutoMapper(typeof(Program));

// Serilog
var logger = new LoggerConfiguration()
 .ReadFrom.Configuration(builder.Configuration)
 .Enrich.FromLogContext()
 .CreateLogger();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

// Repositorios
builder.Services.AddScoped<TransferenciaRepository>();

// Servicios
builder.Services.AddScoped<TransferenciaService>();

var app = builder.Build();

//using (var scope = app.Services.CreateScope()) { 
//    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
//    context.Database.Migrate();
//}

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
