
using Microsoft.AspNetCore.Http.Features;

namespace API.Data
{
    public static class DataSeedExtensions
    {
        public static async void UseDataSeed(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var dataSeed = serviceScope.ServiceProvider.GetService<DataSeed>();
                await dataSeed!.Seed();
            }
        }
    }
}
