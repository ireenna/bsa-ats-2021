using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using WebAPI.Extensions;

namespace WebAPI
{
    public class Program
    {
        public async static Task Main(string[] args)
        {
            (await (
                (await CreateHostBuilder(args)
                    .Build()
                    .ApplyDatabaseMigrations()
                    .ApplyElasticSeeding()
                ).ApplyVacancySeeding()
            ))
            .Run();

        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
