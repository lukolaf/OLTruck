using OLTruck.Domain.Enums;
using OLTruck.Domain.Models;

namespace OLTruck.Infrastructure
{
    public class DataInitializer
    {
        public static void InitializeData(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<OlTruckDbContext>();
                if (context?.Database.ProviderName == "Microsoft.EntityFrameworkCore.InMemory")
                {
                    if (!context.Trucks.Any())
                    {
                        context.Trucks.AddRange(
                            new Truck { Code = "WWA100", Name = "Volvo", Status = TruckStatus.Loading, Description = "Test Truck 1" },
                            new Truck { Code = "WWA00", Name = "Man", Status = TruckStatus.AtJob, Description = "Test Truck 2" },
                            new Truck { Code = "TRK300", Name = "Scania", Status = TruckStatus.ToJob, Description = "Test Truck 3" },
                            new Truck { Code = "TRK400", Name = "Skoda", Status = TruckStatus.Returning, Description = "Test Truck 4" });

                        context.SaveChanges();
                    }
                }
            }
        }
    }
}
