using Microsoft.EntityFrameworkCore;
using RegistryBackend;
using RegistryBackend.Business;
using RegistryBackend.BusinessModel;
using RegistryBackend.DatabaseSeeding;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//Dependency injection pour le context de base de données
var connectionString = builder.Configuration.GetConnectionString("Registry") ?? "Data Source=Registry.db";
builder.Services.AddDbContext<RegistryDb>(
    options => options.UseSqlite(connectionString)
);

//Ajout des services en tant que scoped pour qu'ils puissent être passés en DI
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<ISaleService, SaleService>();
builder.Services.AddScoped<ICheckoutService, CheckoutService>();

//Database seeding
builder.Services.AddScoped<DbInitializer>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDataSeeder();
}

app.UseCors(
    options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()
);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action}/{id?}");

app.Run();
