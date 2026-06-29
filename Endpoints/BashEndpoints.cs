using bach_bash.DTOs.BashDtos;
using bach_bash.Services;

namespace bach_bash.Endpoints;

public static class BashEndpoints
{
    public static void MapBashEndPoints(this IEndpointRouteBuilder routes)
    {
        var bashApi = routes.MapGroup("/api/bashes").WithTags("Bashes");

        bashApi.MapPost("/", async(IBashService service, CreateBashDto command) =>
        {
            var bash = await service.CreateBashAsync(command);
            return TypedResults.Created($"/api/bashes/{bash.Id}", bash);
        });

        bashApi.MapGet("/", async (IBashService service) =>
        {
            var bashes = await service.GetAllBashesAsync();
            return TypedResults.Ok(bashes);
        });

        bashApi.MapGet("/{bashId}", async (IBashService service, Guid bashId) =>
        {
            var bash = await service.GetBashByIdAsync(bashId);
            return bash is null
                ? (IResult)TypedResults.NotFound(new { Message = $"bash with ID {bashId} not found." })
                : TypedResults.Ok(bash);
        });
        
        bashApi.MapGet("/owned/{bashMemberId}", async (IBashService service, Guid bashMemberId) =>
        {
            var bashes = await service.GetAllBashesByOwnerAsync(bashMemberId);
            return bashes is null
                ? (IResult)TypedResults.NotFound(new { Message = $"bash with owner ID {bashMemberId} not found." })
                : TypedResults.Ok(bashes);
        });
        
        bashApi.MapPut("/{bashId}", async (IBashService service, Guid bashId, UpdateBashDto command) =>
        {
            await service.UpdateBashAsync(bashId, command);
            return TypedResults.NoContent();
        });

        bashApi.MapDelete("/{bashId}", async (IBashService service, Guid bashId) =>
        {
            await service.DeleteBashAsync(bashId);
            return TypedResults.NoContent();
        });
    }
}