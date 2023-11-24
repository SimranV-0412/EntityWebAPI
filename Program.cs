using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Web;
using Microsoft.OpenApi.Models;
using System.Net;
using WebAPI_Project.DAL;
using Swashbuckle.AspNetCore.Filters;
using static WebAPI_Project.DAL.MyAppDBContext;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<IConfiguration>(builder.Configuration);

// Add services to the container.
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    options.OperationFilter<SecurityRequirementsOperationFilter>();
});

builder.Services.AddSwaggerGen();
//builder.Services.AddTransient<MyAppDBContext>();


builder.Services.AddDbContext<MyAppDBContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("Con")));

//builder.Services.AddAuthorization(options =>
//{
//    // Authorization policies configuration
//});




builder.Services.AddAuthorization();
//builder.Services.AddIdentityCore<IdentityUser>()
//    .AddEntityFrameworkStores<MyAppDBContext>();
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    // Identity options configuration if needed
})
    .AddEntityFrameworkStores<MyAppDBContext>()
.AddDefaultTokenProviders();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.MapIdentityApi<IdentityUser>();
app.MapSwagger().RequireAuthorization();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
