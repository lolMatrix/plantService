using DataBase;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using StatisticAndSolutions;
using System.Text.Json.Serialization;

namespace PlantService
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                              builder =>
                              {
                                  builder
                                    .AllowAnyMethod()
                                    .AllowAnyOrigin()
                                    .AllowAnyHeader();
                              });
            });
            services.AddRouting();
            services.AddControllers()
                .AddJsonOptions(c => 
                {
                    var enumConverter = new JsonStringEnumConverter();
                    c.JsonSerializerOptions.Converters.Add(enumConverter);
                });
            services.AddMvcCore();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "PlanService API",
                    Description = "?????? ??? ????? ?????? ? ??????",
                    Contact = new OpenApiContact
                    {
                        Name = "?????? ???????, ???? ?????????, ?????? ????????",
                        Url = new Uri("https://github.com/lolMatrix/plantService"),
                        Email = String.Empty
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Use under GNU GPL V3",
                        Url = new Uri("https://antirao.ru/gpltrans/gplru.pdf"),
                    }
                });

                var filePath = Path.Combine(AppContext.BaseDirectory, "PlantService.xml");
                c.IncludeXmlComments(filePath);
            });

            services.AddSingleton<Context>();
            services.AddSingleton(typeof(Repository<>));
            services.AddTransient<StatisticHandler>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseSwagger(c =>
            {
                c.SerializeAsV2 = true;
            });
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "PlanService V1");
                c.RoutePrefix = string.Empty;
            });

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
