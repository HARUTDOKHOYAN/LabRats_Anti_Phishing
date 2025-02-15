using System.Text;
using LearningASPweb.GlobalManagers.Contracts;
using LearningASPweb.Data.Repositories;
using Microsoft.AspNetCore.Identity;
using LearningASPweb.GlobalManagers;
using LearningASPweb.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using AntiPhishingAPI.SerVices.ServiceInterfaces;
using AntiPhishingAPI.SerVices.ServiceClasses;

namespace LearningASPweb.Configurations;

public static class ServiceLocatorConfig
{
    public static void ConfigureAppServices(WebApplicationBuilder builder)
    {
        var services = builder.Services;
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        ConfigureatSwagger(services);
        AddJwtService(builder);
        services.AddAutoMapper(typeof(MapperConfig));
        services.AddIdentityCore<APIUserModel>()
            .AddRoles<IdentityRole>()
            .AddTokenProvider<DataProtectorTokenProvider<APIUserModel>>("TumoLubCybersecurityAPI")
            .AddEntityFrameworkStores<AnitPhoshingDbContext>()
            .AddDefaultTokenProviders();
        services.AddScoped<IAuthManager, AuthManager>();
        services.AddScoped<IPhishingChecker, PhishingChecker>();
        services.AddApiVersioning(option =>
        {
            option.AssumeDefaultVersionWhenUnspecified = true;
            option.ReportApiVersions = true;
            option.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
            option.ApiVersionReader = ApiVersionReader.Combine(
                new QueryStringApiVersionReader("api-version"),
                new HeaderApiVersionReader("X-Api-Version"),
                new MediaTypeApiVersionReader("ver")
            );
        });
        builder. Services.AddVersionedApiExplorer( options =>
        {
            options.GroupNameFormat = "'v'VVV";
            options.SubstituteApiVersionInUrl = true; 
        }); 
    }

    private static void ConfigureatSwagger(IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo() { Title = "HotelAPI", Version = "v1" });
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer 12345abcdef'",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = JwtBearerDefaults.AuthenticationScheme
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement()
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = JwtBearerDefaults.AuthenticationScheme
                        },
                        Scheme = "oauth2",
                        Name = JwtBearerDefaults.AuthenticationScheme,
                        In = ParameterLocation.Header,
                    },
                    new List<string>()
                }
            });
        });
    }

    private static void AddJwtService(WebApplicationBuilder builder)
    {
        builder.Services.AddAuthentication(
            options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuerSigningKey = true,
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero,
                ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
                ValidAudience = builder.Configuration["JwtSettings:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Key"]))  };
        });
    }
}