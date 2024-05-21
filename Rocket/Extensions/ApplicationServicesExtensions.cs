using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Rocket.Core.Entities.Identity;
using Rocket.Core.Repository;
using Rocket.Core.Services;
using Rocket.DTOS;
using Rocket.Errors;
using Rocket.Helper;
using Rocket.Repository;
using Rocket.Services;
using System.Linq;

namespace Rocket.Extensions
{
    public static class ApplicationServicesExtensions
    {
        public static IServiceCollection AddAppServices( this IServiceCollection services)
        {
            services.AddScoped<IPaymentServices, PaymentServices>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IOrderService,OrderServices>();
            services.AddScoped(typeof(ITokenServices), typeof(TokenServices));

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            //services.AddAutoMapper(m => m.AddProfile(new MappingProfiles));
            services.AddAutoMapper(typeof(MappingProfiles));

            services.AddScoped(typeof(IBasketRepository), typeof(BasketRepository));

            services.Configure<ApiBehaviorOptions>(options =>
             options.InvalidModelStateResponseFactory = (actioncontext) =>
             {
                 var error = actioncontext.ModelState.Where(m => m.Value.Errors.Count() > 0)
                                                     .SelectMany(m => m.Value.Errors)
                                                     .Select(e => e.ErrorMessage)
                                                     .ToArray();
                 var validerrorresponse = new ApiValidationErrorResponse() { Errors = error };
                 return new BadRequestObjectResult(validerrorresponse);
             });
            

            return services;

        }
    }
}
