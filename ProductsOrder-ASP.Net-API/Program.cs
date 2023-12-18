using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ProductsOrder_ASP.Net_API.Data;
using ProductsOrder_ASP.Net_API.Extensions;
using ProductsOrder_ASP.Net_API.Models;
using ProductsOrder_ASP.Net_API.Services;
using ProductsOrder_ASP.Net_API.Services.IServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// connection to database
/*builder.Services.AddDbContext<ApplicationDBContext>(options =>
{
 options.UseSqlServer(builder.Configuration.GetConnectionString("Connection"));
});
*/

builder.Services.AddDbContext<ApplicationDBContext>(options =>
   options.UseSqlServer(builder.Configuration.GetConnectionString("Connection")));

builder.Services.AddScoped<IProduct, ProductsServices>();
builder.Services.AddScoped<IOrder, OrdersService>();
builder.Services.AddScoped<IUser, UserService>();
builder.Services.AddScoped<IJwt, JwtService>();

builder.AddSwaggenGenExtension();   

//Automapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.AddAuth();
builder.AddAdminPolicy();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();    

app.UseAuthorization();

app.MapControllers();

app.Run();
