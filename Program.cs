global using System;
global using System.Text;
global using Sneaker_Shop_API.Data;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Sneaker_Shop_API.Authorization;
using Sneaker_Shop_API.Services;
using Sneaker_Shop_API.Settings;
using Swashbuckle.AspNetCore.Filters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<AppSettings>(builder.Configuration);
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("Default"));
});
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = "Authorization header with bearer scheme",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    options.OperationFilter<SecurityRequirementsOperationFilter>();
});
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey =
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("Jwt:Key").Value)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
        options.Events = new JwtBearerEvents()
        {
            OnTokenValidated = async context =>
            {
                //get userid if type is "userid"
                var email = context.Principal.Claims.Where(x => x.Type == ClaimTypes.Email).FirstOrDefault().Value;
                var authService = context.HttpContext.RequestServices.GetRequiredService<AuthService>();
                var existingUser = await authService.GetByEmail(email);
                if (existingUser == null )
                {
                    context.Fail("invaild token");
                }
            },

        };
    });

// Add Custom services
builder.Services.AddScoped<JwtUtils>();
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<SneakerService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.DocumentTitle = "Sneaker Shop API";
        options.EnablePersistAuthorization();
    });
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();