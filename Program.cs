
using Microsoft.EntityFrameworkCore;
using ProductService.Infrastructure.Data;


var builder = WebApplication.CreateBuilder(args);

//var connectionString = builder.Configuration.GetConnectionString("ConexaoPadrao");


// 🔐 Carrega configurações do appsettings.json
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

// 🧱 Adiciona serviços ao container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 🗄️ Configura EF Core com PostgreSQL
builder.Services.AddDbContext<EstoqueDbContext >(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("ConexaoPadrao")));

// 🔨 Constrói o app
var app = builder.Build();

// 📄 Habilita Swagger
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "SistemaHospedagem API v1");
    c.RoutePrefix = string.Empty; // Abre Swagger direto na raiz
});

// 🔐 Middleware padrão
app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();