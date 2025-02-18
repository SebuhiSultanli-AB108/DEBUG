using DEBUG.BL.Exceptions;
using DEBUG.BL.ExternalServices;
using DEBUG.BL.Services.AdditionalServices;
using DEBUG.BL.Services.AnswerServices;
using DEBUG.BL.Services.CategoryServices;
using DEBUG.BL.Services.CommentServices;
using DEBUG.BL.Services.QuestionServices;
using DEBUG.BL.Services.QuizQuestionServices;
using DEBUG.BL.Services.ReportServices;
using DEBUG.BL.Services.TagServices;
using DEBUG.BL.Services.UserServices;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace DEBUG.BL;

public static class ServiceRegistrationsBL
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IJWTTokenHandler, JWTTokenHandler>();
        services.AddScoped<IQuestionService, QuestionService>();
        services.AddScoped<IAnswerService, AnswerService>();
        services.AddScoped<ICommentService, CommentService>();
        services.AddScoped<ITagService, TagService>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<IQuizQuestionService, QuizQuestionService>();
        services.AddScoped<IReportService, ReportService>();
        services.AddScoped<ILikeDislikeService, LikeDislikeService>();
        return services;
    }

    public static IServiceCollection AddMapperProfiles(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(ServiceRegistrationsBL));
        return services;
    }

    public static IServiceCollection AddFluentValidation(this IServiceCollection services)
    {
        services.AddFluentValidationAutoValidation();
        services.AddValidatorsFromAssemblyContaining(typeof(ServiceRegistrationsBL));
        return services;
    }

    public static IApplicationBuilder UseExceptionHandler(this IApplicationBuilder app)
    {
        app.UseExceptionHandler(handler =>
        {
            handler.Run(async context =>
            {
                Exception exception = context.Features.Get<IExceptionHandlerFeature>()!.Error;
                if (exception is IBaseException ibe)
                {
                    await context.Response.WriteAsJsonAsync(new
                    {
                        StatusCode = ibe.Code,
                        Message = ibe.ErrorMessage
                    });
                }
                else
                {
                    await context.Response.WriteAsJsonAsync(new
                    {
                        StatusCode = StatusCodes.Status400BadRequest,
                        Message = exception.Message
                    });
                }
            });
        });

        return app;
    }
}
