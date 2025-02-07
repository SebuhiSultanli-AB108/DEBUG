﻿using DEBUG.Core.Enums;
using DEBUG.Core.Models;
using DEBUG.Core.RepositoryInstances;
using DEBUG.DAL.Context;
using DEBUG.DAL.RepositoryImplements;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DEBUG.DAL;

public static class ServiceRegistrationsDAL
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IQuestionRepository, QuestionRepository>();
        services.AddScoped<IAnswerRepository, AnswerRepository>();
        services.AddScoped<ICommentRepository, CommentRepository>();
        return services;
    }

    public static IServiceCollection AddServer(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(opt =>
        {
            opt.UseSqlServer(configuration.GetConnectionString("Phoenix"));
        });
        return services;
    }

    public static IServiceCollection AddIdentitySettings(this IServiceCollection services)
    {
        services.AddIdentity<User, IdentityRole>(opt =>
        {
            opt.User.RequireUniqueEmail = true;
            opt.SignIn.RequireConfirmedEmail = false;//

            opt.Password.RequiredUniqueChars = 3;
            opt.Password.RequiredLength = 8;
            opt.Password.RequireNonAlphanumeric = true;
            opt.Password.RequireUppercase = true;
            opt.Password.RequireLowercase = true;

            opt.Lockout.AllowedForNewUsers = true;
            opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
            opt.Lockout.MaxFailedAccessAttempts = 3;

        })
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders(); ;
        return services;
    }


    public static IApplicationBuilder AddSeedData(this IApplicationBuilder app)
    {
        using (var scope = app.ApplicationServices.CreateScope())
        {
            var _userManger = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
            var _roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            CreateRoles(_roleManager).Wait();
            CreateAdmin(_userManger).Wait();
        }
        return app;
    }

    private static async Task CreateRoles(RoleManager<IdentityRole> _roleManager)
    {
        int res = await _roleManager.Roles.CountAsync();

        if (res == 0)
        {
            foreach (var role in Enum.GetValues(typeof(Roles)))
            {
                await _roleManager.CreateAsync(new IdentityRole(role.ToString()));
            }
        }
    }

    private static async Task CreateAdmin(UserManager<User> _userManager)
    {
        if (!await _userManager.Users.AnyAsync(x => x.UserName == "admin"))
        {
            User user = new User
            {
                UserName = "admin",
                FullName = "admin",
                Email = "admin@gmail.com",
            };
            user.EmailConfirmed = true;
            await _userManager.CreateAsync(user, "Phoenix_0556");
            await _userManager.AddToRoleAsync(user, nameof(Roles.Admin));
        }
    }
}