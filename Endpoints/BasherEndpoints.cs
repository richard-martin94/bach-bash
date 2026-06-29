using bach_bash.DTOs.BasherDtos;
using bach_bash.Services;

namespace bach_bash.Endpoints;

public static class BasherEndpoints
{
    public static void MapBasherEndPoints(this IEndpointRouteBuilder routes)
    {
        var basherApi = routes.MapGroup("/api/bashers").WithTags("Bashers");

        basherApi.MapPost("/", async(IBasherService service, CreateBasherDto command) =>
        {
            var basher = await service.CreateBasherAsync(command);
            return TypedResults.Created($"/api/bashers/{basher.Username}", basher);
        });

        basherApi.MapGet("/basher/{basherId}", async (IBasherService service, Guid basherId) =>
        {
            var basher = await service.GetBasherByIdAsync(basherId);
            return TypedResults.Ok(basher);
        });

        basherApi.MapGet("/", async (IBasherService service) =>
        {
            var bashers = await service.GetAllBashersAsync();
            return TypedResults.Ok(bashers);
        });

        basherApi.MapGet("/bash/{bashId}", async (IBasherService service, Guid bashId) =>
        {
            var bashers = await service.GetAllBashersByBashAsync(bashId);
            return bashers is null
                ? (IResult)TypedResults.NotFound(new { Message = $"basher from bash id {bashId} not found." })
                : TypedResults.Ok(bashers);
        });

        basherApi.MapPut("/{basherId}", async (IBasherService service, Guid basherId, UpdateBasherDto command) =>
        {
            await service.UpdateBasherAsync(basherId, command);
            return TypedResults.NoContent();
        });

        basherApi.MapDelete("/{basherId}", async (IBasherService service, Guid basherId) =>
        {
            await service.DeleteBasherAsync(basherId);
            return TypedResults.NoContent();
        });
    }
}