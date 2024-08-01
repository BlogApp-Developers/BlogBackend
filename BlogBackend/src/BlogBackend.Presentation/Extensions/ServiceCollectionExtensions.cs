using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogBackend.Core.Role.Models;
using BlogBackend.Core.User.Models;
using BlogBackend.Infrastructure.Data.DbContext;
using BlogBackend.Presentation.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace BlogBackend.Presentation.Extensions;

public static class ServiceCollectionExtensions
{
    public static void InitAspnetIdentity(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection.AddDbContext<BlogDbContext>(options =>
        {
            var connectinoString = configuration.GetConnectionString("PostgreSqlDev");
            options.UseNpgsql(connectinoString);
        });

        serviceCollection.AddIdentity<User, Role>()
            .AddEntityFrameworkStores<BlogDbContext>();
    }

    public static void InitAuth(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        var jwtSection = configuration.GetSection("Jwt");
        serviceCollection.Configure<JwtOptions>(jwtSection);

        serviceCollection.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
        })
            .AddJwtBearer(options =>
            {
                var jwtOptions = jwtSection.Get<JwtOptions>();

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = "MyApplication",

                    ValidateAudience = true,
                    ValidAudience = "Big Company",

                    ValidateLifetime = true,

                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(jwtOptions!.KeyInBytes)
                };
            });

        serviceCollection.AddAuthorization(options => {
            options.AddPolicy(
                "BlogPolicy",
                (policyBuilder) => {
                    policyBuilder.RequireRole("Admin", "IT", "Developer");
                }
            );
        });
    }

    public static void InitSwagger(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo()
            {
                Title = "Jwt Identity Service",
                Version = "v1",
            });

            options.AddSecurityDefinition(
                name: JwtBearerDefaults.AuthenticationScheme,
                securityScheme: new OpenApiSecurityScheme()
                {
                    Description = "Input yout JWT token here:",
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = JwtBearerDefaults.AuthenticationScheme,
                });

            options.AddSecurityRequirement(
                new OpenApiSecurityRequirement() {
                {
                    new OpenApiSecurityScheme() {
                        Reference = new OpenApiReference() {
                            Id = JwtBearerDefaults.AuthenticationScheme,
                            Type = ReferenceType.SecurityScheme
                        }
                    },
                    new string[] {}
                }
                }
            );
        });
    }

    public static void InitCors(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddCors(options =>
        {
            options.AddPolicy("BlazorApp", policyBuilder =>
            {
                policyBuilder
                    .WithOrigins("http://localhost:5025")
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            });
        });
    }
}
