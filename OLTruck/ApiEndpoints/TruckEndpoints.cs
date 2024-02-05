using MediatR;
using OLTruck.Commands;
using OLTruck.Domain.Enums;
using OLTruck.Queries;
using OLTruck.Shared.TruckDto;

namespace OLTruck.ApiEndpoints
{
    public static class TruckEndpoints
    {
        public static void MapTruckEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapPost("/trucks", async (IMediator mediator, TruckCreateDto truck) =>
            {
                var command = new CreateTruckCommand { TruckCreateDto = truck };
                var result = await mediator.Send(command);

                return Results.Created($"/trucks/{truck.Code}", truck);
            });

            app.MapGet("/trucks", async (IMediator mediator) =>
            {
                var trucks = await mediator.Send(new GetAllTrucksQuery());
                return Results.Ok(trucks);
            });

            app.MapGet("/trucks/{code}", async (IMediator mediator, string code) =>
            {
                var query = new GetTruckByCodeQuery(code);
                try
                {
                    var truck = await mediator.Send(query);
                    return Results.Ok(truck);
                }
                catch (KeyNotFoundException)
                {
                    return Results.NotFound();
                }
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
