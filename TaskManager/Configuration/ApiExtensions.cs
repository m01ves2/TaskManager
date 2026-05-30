using Microsoft.AspNetCore.Mvc;
using TaskManager.Errors;

namespace TaskManager.Configuration
{
    public static class ApiExtensions
    {
        public static IServiceCollection AddApi(this IServiceCollection services)
        {
            services.AddControllers()
                .ConfigureApiBehaviorOptions(options =>
                {
                    options.InvalidModelStateResponseFactory = context =>
                    {
                        var errors = context.ModelState
                            .Where(x => x.Value?.Errors.Count > 0)
                            .ToDictionary(
                                x => x.Key,
                                x => x.Value!.Errors.Select(e => e.ErrorMessage).ToArray()
                            );

                        return new BadRequestObjectResult(
                            new ValidationErrorResponse()
                            {
                                Code = ErrorCodes.ValidationError,
                                Message = "Validation failed",
                                Errors = errors
                            }
                        );
                    };
                });

            services.AddSwaggerGen();

            return services;
        }
    }
}
