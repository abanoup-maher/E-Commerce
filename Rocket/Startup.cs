using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Rocket.Repository.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Rocket.Core.Repository;
using Rocket.Repository;
using Rocket.Helper;
using Rocket.Errors;
using Rocket.Milddlewares;
using Rocket.Extensions;
using StackExchange.Redis;
using Rocket.Repository.Identity;

namespace Rocket
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            //services.AddSwaggerGen(c =>
            //{
            //    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Rocket", Version = "v1" });
            //});
            services.AddSwaggerservices();

            services.AddDbContext<RocketStoreDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddDbContext<AppIdentityDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("IdentityConnection"));
            });

            services.AddSingleton<IConnectionMultiplexer>(s =>
            {
                var connection = ConfigurationOptions.Parse(Configuration.GetConnectionString("redis"));
                return ConnectionMultiplexer.Connect(connection);
            });

            //services.AddScoped(typeof(IGenericRepository<>),typeof(GenericRepository<>));
            ////services.AddAutoMapper(m => m.AddProfile(new MappingProfiles));
            //services.AddAutoMapper(typeof(MappingProfiles));

            //services.Configure<ApiBehaviorOptions>(options =>
            // options.InvalidModelStateResponseFactory = (actioncontext) =>
            // {
            //     var error = actioncontext.ModelState.Where(m => m.Value.Errors.Count() > 0)
            //                                         .SelectMany(m => m.Value.Errors)
            //                                         .Select(e => e.ErrorMessage)
            //                                         .ToArray();
            //     var validerrorresponse = new ApiValidationErrorResponse() { Errors = error };
            //     return new BadRequestObjectResult(validerrorresponse);
            // });
            // call function ale 3mltha btlm kol el services

            services.AddAppServices();
            services.AddIdentityServices(Configuration);

           



        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            app.UseMiddleware<ExceptionMiddleWare>();

            if (env.IsDevelopment())
            {
                ////app.UseDeveloperExceptionPage();
                //app.UseSwagger();
                //app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Rocket v1"));
                app.UseSwaggerDocumentation();
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
           
            app.UseStaticFiles();

           

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
