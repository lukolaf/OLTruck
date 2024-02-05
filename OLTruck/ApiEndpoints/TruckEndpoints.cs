using MediatR;
using Microsoft.AspNetCore.Components.Forms;
using OLTruck.Commands;
using OLTruck.Domain.Enums;
using OLTruck.Queries;
using OLTruck.Shared.TruckDto;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

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

            app.MapGet("/trucks", async (IMediator mediator, string? filter, string? sortBy) =>
            {
                var trucks = await mediator.Send(new GetAllTrucksQuery { Filter = filter, SortBy = sortBy });
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

            app.MapDelete("/trucks/{code}", async (IMediator mediator, string code) =>
            {
                var command = new DeleteTruckCommand(code);
                try
                {
                    await mediator.Send(command);
                    return Results.NoContent();
                }
                catch (KeyNotFoundException ex)
                {
                    return Results.NotFound(ex.Message);
                }
            });

            app.MapPatch("/trucks/{code}/status", async (IMediator mediator, string code, TruckStatus newStatus) =>
            {
                var command = new UpdateTruckStatusCommand(code, newStatus);
                try
                {
                    await mediator.Send(command);
                    return Results.NoContent();
                }
                catch (KeyNotFoundException ex)
                {
                    return Results.NotFound(ex.Message);
                }
            });
        }
    }
}
