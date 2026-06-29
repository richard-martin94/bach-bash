using bach_bash.DTOs.BashMemberDtos;
using bach_bash.Services;

namespace bach_bash.Endpoints;

public static class BashMemberEndpoints
{
    public static void MapBashMemberEndPoints(this IEndpointRouteBuilder routes)
    {
        var bashMemberApi = routes.MapGroup("/api/bashmembers").WithTags("BashMembers");

        bashMemberApi.MapPost("/", async (IBashMemberService service, CreateBashMemberDto command) =>
        {
            var bashMember = await service.CreateBashMemberAsync(command);
            return TypedResults.Created($"/api/bashmembers/{bashMember.BashId}/{bashMember.BasherId}", bashMember);
        });
        
        bashMemberApi.MapGet("/{bashMemberId}", async (IBashMemberService service, Guid bashMemberId) =>
        {
            var bashes = await service.GetBashesByMemberIdAsync(bashMemberId);
            return bashes is null
                ? (IResult)TypedResults.NotFound(new { Message = $"bashes with member ID {bashMemberId} not found." })
                : TypedResults.Ok(bashes);
        });
        
        bashMemberApi.MapGet("/bash/{bashId}", async (IBashMemberService service, Guid bashId) =>
        {
            var bashMembers = await service.GetAllBashMembersByBashAsync(bashId);
            return bashMembers is null
                ? (IResult)TypedResults.NotFound(new { Message = $"members with bash ID {bashId} not found." })
                : TypedResults.Ok(bashMembers);
        });

        bashMemberApi.MapDelete("/bash/{bashId}", async (IBashMemberService service, Guid bashId) =>
        {
            await service.DeleteAllBashMembersByBashAsync(bashId);
            return TypedResults.NoContent();
        });
        
        bashMemberApi.MapDelete("/member/{bashMemberId}", async (IBashMemberService service, Guid bashMemberId) =>
        {
            await service.DeleteBasherFromAllBashesAsync(bashMemberId);
            return TypedResults.NoContent();
        });
        
        bashMemberApi.MapDelete("/member-bash/{bashMemberId}/{bashId}", async (IBashMemberService service,Guid bashMemberId, Guid bashId) =>
        {
            await service.DeleteBashMemberFromOneBashAsync(bashMemberId, bashId);
            return TypedResults.NoContent();
        });
    }
}