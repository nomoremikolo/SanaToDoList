using AspDotNetProject.Enum;
using AspDotNetProject.Models;
using BusinessLogic.intefaces;
using MSQLDataRepository;
using XMLDataRepository;

using AspDotNetProject.GraphQL;
using GraphQL.SystemTextJson;
using GraphQL.MicrosoftDI;
using GraphQL;
using GraphQL.Server;
using GraphQL.Server.Ui.Playground;
using AspDotNetProject.Extensions;

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

        services.AddGraphQLApi();

        services.AddTransient<IRepository>(provider =>
        {
            switch (DataBaseProvider.DBIndetificator)
            {
                case (int)DataBaseEnum.XML:
                    return new XMLRepository();
                case (int)DataBaseEnum.MSSQL:
                    return new MSSqlRepository(configuration["ConnectionStrings:default"]);
            }
            return new XMLRepository();
        });
    }
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
            app.UseDeveloperExceptionPage();

        app.ApplicationServices.CreateScope();

        app.UseGraphQLPlayground();
        app.UseGraphQL<ToDoSchema>();
        app.UseGraphQLAltair();
        app.UseGraphQLGraphiQL();

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