using DEBUG.BL.DTOs.OptionsDTOs;
using Microsoft.OpenApi.Models;

namespace DEBUG.API;

public static class ServiceRegistrationAPI
{
    public static IServiceCollection AddJwtOptions(this IServiceCollection services, IConfiguration _configuration)
    {
        services.Configure<JWTOption>(_configuration.GetSection(JWTOption.Jwt));
        return services;
    }
    public static IServiceCollection AddSmtpOptions(this IServiceCollection services, IConfiguration _configuration)
    {
        services.Configure<SmtpOption>(_configuration.GetSection(SmtpOption.Smtp));
        return services;
    }
    public static IServiceCollection ConfigureSwaggerAuthentication(this IServiceCollection services)
    {

        services.AddSwaggerGen(opt =>
        {
            opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
            {
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer"
            });

            opt.AddSecurityRequirement(new OpenApiSecurityRequirement()
            {
                {
                    new OpenApiSecurityScheme {
                        Reference = new OpenApiReference {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    }, Array.Empty<string>()
                }
            });
        });
        return services;
    }
}