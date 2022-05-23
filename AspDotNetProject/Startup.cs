using AspDotNetProject.GraphQL;
using GraphQL;
using AspDotNetProject.Extensions;
using AspDotNetProject;
using Microsoft.AspNetCore.Server.Kestrel.Core;

public class Startup
{
    private readonly IConfiguration configuration;
    public Startup(IConfiguration configuration)
    {
        this.configuration = configuration;
    }
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllersWithViews();
        services.AddHttpContextAccessor();

        services.Configure<IISServerOptions>(options => options.AllowSynchronousIO = true);
        services.Configure<KestrelServerOptions>(options => options.AllowSynchronousIO = true);

        services.AddGraphQLApi();
        services.AddProviderService(configuration);

        services.AddAutoMapper(typeof(MapperProfile));
    }
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
            app.UseDeveloperExceptionPage();

        app.ApplicationServices.CreateScope();

        app.UseGraphQLPlayground();
        app.UseGraphQL<ToDoListSchema>();
        app.UseGraphQLAltair();

        app.UseRouting();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=ToDoList}/{action=Index}"
                );
        });
        app.UseStaticFiles();
    }
}