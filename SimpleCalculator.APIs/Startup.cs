using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SimpleCalculator.Lib;
using SimpleCalculator.Log;
using SimpleCalculator.Log.DataModel;
using System;
using System.IO;
using System.Reflection;

namespace SimpleCalculator.APIs
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
            services.AddSingleton<ISimpleCalculator, SimpleCalculatorLib>();
            //services.AddSingleton<ISimpleCalculatorLogger, SimpleCalculatorEFLogger>();
            //services.AddSingleton<ISimpleCalculatorLogger, SimpleCalculatorADOLogger>();

            services.AddControllers();

            //Add Swagger specification to the pipeline.
            services.AddSwaggerGen(setupAction =>
            {
                setupAction.SwaggerDoc(
                    "SimpleCalculatorAPISpecification",
                    new Microsoft.OpenApi.Models.OpenApiInfo()
                    {
                        Title = "SimpleCalculator API",
                        Version = "1"
                    });
                var commentfile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                commentfile = Path.Combine(AppContext.BaseDirectory, commentfile);
                setupAction.IncludeXmlComments(commentfile);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();
            //Add swagger UI and remove the swagger prefix
            app.UseSwagger();
            app.UseSwaggerUI(setupAction =>
            {
                setupAction.SwaggerEndpoint("/swagger/SimpleCalculatorAPISpecification/swagger.json", "SimpleCalculator API");
                setupAction.RoutePrefix = "";
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
