using AspDotNetProject.Enum;
using AspDotNetProject.Models;
using BusinessLogic.intefaces;
using MSQLDataRepository;
using XMLDataRepository;

namespace AspDotNetProject.Extensions
{
    public static class DataBaseProviderService
    {
        public static IServiceCollection AddProviderService(this IServiceCollection services, IConfiguration configuration)
        {
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

            return services;
        }
    }
}
