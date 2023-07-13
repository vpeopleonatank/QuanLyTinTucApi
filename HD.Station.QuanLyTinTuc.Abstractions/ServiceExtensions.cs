using System.Reflection;
using Slugify;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
using HD.Station.QuanLyTinTuc.Abstractions.DTO;

public static class ServiceExtensions
{
    public static IServiceCollection AddAbstractions(this IServiceCollection services)
    {
        return services
          // .AddFluentValidationAutoValidation()
          // .AddFluentValidationClientsideAdapters()
          .AddValidatorsFromAssemblyContaining<RegisterValidator>()
          .AddScoped<ISlugHelper, SlugHelper>();
            // .AddValidatorsFromAssemblyContaining(typeof(RegisterValidator));
    }
}
