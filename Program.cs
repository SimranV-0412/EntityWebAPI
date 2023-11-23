using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web;
using WebAPI_Project.DAL;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<MyAppDBContext>();

//builder.Services.AddDbContext<MyAppDBContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("Con")));

builder.Services.AddDbContext<MyAppDBContext>
(options => options.UseSqlServer("Data Source=MCNDESKTOP33;Initial Catalog=WebApi;User ID=sa;Password=Password$2;TrustServerCertificate=True;"));

var c= builder.Configuration.GetConnectionString("Con");

//builder.Services.AddAuthorization(options =>
//{
//    // Authorization policies configuration
//});

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
