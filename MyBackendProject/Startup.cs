using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBackendProject
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
            
            // adds NSwag middleware into middleware pipelie
            services.AddSwaggerDocument(options =>
            {
                // document name is dogs and version is V1
                options.DocumentName = "Dogs";
                options.Version = "V1";
            });
            services.AddHttpClient(Configuration["DogClientName"], configureClient: client =>
            {
            client.BaseAddress = new Uri(Configuration["DogAddress"]);
            client.DefaultRequestHeaders.Add("x-api-key", Configuration["XApiKey"] ); // configure HttpClient with an api key
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseOpenApi(); // configures app to run an OpenApi specification generator upon start up
            app.UseSwaggerUi3(); // adds Swagger Ui to app
        }
    }
}
