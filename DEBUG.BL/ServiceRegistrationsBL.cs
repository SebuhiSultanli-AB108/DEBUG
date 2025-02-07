using DEBUG.BL.Services.AnswerServices;
using DEBUG.BL.Services.QuestionServices;
using Microsoft.Extensions.DependencyInjection;

namespace DEBUG.BL;

public static class ServiceRegistrationsBL
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IQuestionService, QuestionService>();
        services.AddScoped<IAnswerService, AnswerService>();
        //services.AddScoped<ICommentService, CommentService>();
        return services;
    }

    public static IServiceCollection AddMapperProfiles(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(ServiceRegistrationsBL));
        return services;
    }
}
