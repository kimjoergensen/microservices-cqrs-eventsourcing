namespace UserService.WebApi
{
  using Microsoft.AspNetCore.Builder;
  using Microsoft.AspNetCore.Hosting;
  using Microsoft.AspNetCore.Http;
  using Microsoft.EntityFrameworkCore;
  using Microsoft.Extensions.DependencyInjection;
  using Microsoft.Extensions.Hosting;
  using Microsoft.OpenApi.Models;
  using UserService.WebApi.Data;

  public class Startup
  {
    // This method gets called by the runtime. Use this method to add services to the container.
    // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
    public void ConfigureServices(IServiceCollection services) {
      services.AddControllers();
      services.AddSwaggerGen(controller => {
        controller.SwaggerDoc("v1", new OpenApiInfo {
          Version = "v1",
          Title = "UserService",
          Description = "User Service ASP.NET Core Web API v1"
        });
      });

      services.AddDbContext<UserServiceContext>(options => options.UseSqlite(@"Data Source=user.db"));
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, Data.UserServiceContext dbContext) {
      if (env.IsDevelopment()) {
        dbContext.Database.EnsureCreated();
        app.UseDeveloperExceptionPage();
      }

      app.UseSwagger();

      app.UseSwaggerUI(controller => {
        controller.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1");
        controller.RoutePrefix = string.Empty;
      });

      app.UseRouting();

      app.UseEndpoints(endpoints => {
        endpoints.MapGet("/", async context => {
          await context.Response.WriteAsync("Hello World!");
        });
      });
    }
  }
}
