using API.Infrastructure.Filters;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Linq;

namespace API
{
    public class Startup
    {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddApiVersioning(options => {
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);
            });

            // Swagger service configuration
            services.AddSwaggerGen(options => {
                options.SwaggerDoc("v1", new Info {
                    Version = "v1",
                    Title = "Catalog API",
                    Description = "Internal catalog service gateway",
                });

                options.OperationFilter<RemoveVersionFromParameter>();
                options.DocumentFilter<ReplaceVersionWithExactValueInPath>();

                options.DocInclusionPredicate((version, description) => {
                    if (!description.TryGetMethodInfo(out var methodInfo)) return false;

                    var versions = methodInfo.DeclaringType
                        .GetCustomAttributes(true)
                        .OfType<ApiVersionAttribute>()
                        .SelectMany(attribute => attribute.Versions);

                    var maps = methodInfo.DeclaringType
                        .GetCustomAttributes(true)
                        .OfType<MapToApiVersionAttribute>()
                        .SelectMany(attribute => attribute.Versions)
                        .ToArray();

                    return versions.Any(_version => $"v{_version.ToString()}" == version)
                        && (!maps.Any() || maps.Any(_version => $"v{_version.ToString()}" == version));
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            } else {
                app.UseHsts();
            }

            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseSwagger();
            app.UseSwaggerUI(options =>
                options.SwaggerEndpoint($"/swagger/v1/swagger.json", $"v1"));

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
