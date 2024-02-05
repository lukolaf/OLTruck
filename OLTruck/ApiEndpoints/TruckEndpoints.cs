using MediatR;
using Microsoft.AspNetCore.Components.Forms;
using OLTruck.Commands;
using OLTruck.Domain.Enums;
using OLTruck.Domain.Models;
using OLTruck.Queries;
using OLTruck.Shared.TruckDto;
using System.ComponentModel.DataAnnotations;

namespace OLTruck.ApiEndpoints
{
    public static class TruckEndpoints
    {
        public static void MapTruckEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapPost("/trucks", async (IMediator mediator, TruckCreateDto truck) =>
            {
                try
                {
                    var command = new CreateTruckCommand { TruckCreateDto = truck };
                    var result = await mediator.Send(command);
                    return Results.Created($"/trucks/{result.Code}", result);
                }
                catch (ValidationException ex)
                {
                    return Results.BadRequest(ex.Value);
                }
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

            app.MapPut("/trucks/{code}", async (IMediator mediator, string code, TruckUpdateDto truckUpdateDto) =>
            {
                try
                {
                    var command = new UpdateTruckCommand(code, truckUpdateDto);
                    await mediator.Send(command);
                    return Results.NoContent();
                }
                catch (ValidationException ex)
                {
                    return Results.BadRequest(ex.Value);
                }
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
