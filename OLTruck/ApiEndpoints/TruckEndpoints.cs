using OLTruck.Domain.Enums;
using OLTruck.Services.Interfaces;

namespace OLTruck.ApiEndpoints
{
    public static class TruckEndpoints
    {
        public static void MapTruckEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapPost("/trucks", () =>
            {
                return Results.NoContent();
            });

            app.MapGet("/trucks", async (ITempService tempService) =>
            {
                var trucks = await tempService.GetAllTrucksAsync();

                return Results.Ok(trucks);
            });

            app.MapGet("/trucks/{code}", (string code) =>
            {
                return Results.NoContent();
            });

            app.MapPut("/trucks/{code}", () =>
            {
                return Results.NoContent();
            });

            app.MapDelete("/trucks/{code}", (string code) =>
            {
                return Results.NoContent();
            });

            app.MapPatch("/trucks/{code}/status", (string code, TruckStatus newStatus) =>
            {
                return Results.NoContent();
            });
        }
    }
}
