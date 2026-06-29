using bach_bash.DTOs.ChallengeDtos;
using bach_bash.Services;

namespace bach_bash.Endpoints;

public static class ChallengeEndpoints
{
    public static void MapChallengeEndpoints(this IEndpointRouteBuilder routes)
    {
        var challengeApi = routes.MapGroup("/api/challenges").WithTags("Challenges");

        challengeApi.MapPost("/", async (IChallengeService service, CreateChallengeDto command) =>
        {
            var challenge = await service.CreateChallengeAsync(command);
            return TypedResults.Created($"/api/challenges/{challenge.Id}", challenge);
        });

        challengeApi.MapGet("/{challengeId}", async (IChallengeService service, Guid challengeId) =>
        {
            var challenge = await service.GetChallengeByIdAsync(challengeId);
            return challenge is null
                ? (IResult)TypedResults.NotFound(new { Message = $"challenge with ID {challengeId} not found." })
                : TypedResults.Ok(challenge);
        });
        
        challengeApi.MapGet("/bash/{bashId}", async (IChallengeService service, Guid bashId) =>
        {
            var challenges = await service.GetAllChallengesByBashAsync(bashId);
            return challenges is null
                ? (IResult)TypedResults.NotFound(new { Message = $"challenges from bash ID {bashId} not found." })
                : TypedResults.Ok(challenges);
        });

        challengeApi.MapPut("/{challengeId}", 
            async (IChallengeService service, Guid challengeId, UpdateChallengeDto command) =>
            {
                await service.UpdateChallengeAsync(challengeId, command);
                return TypedResults.NoContent();
            });
        
        challengeApi.MapPut("/bash/{bashId}", 
            async (IChallengeService service, Guid bashId, List<UpdateChallengeDto> command) =>
            {
                await service.UpdateAllChallengesByBashAsync(bashId, command);
                return TypedResults.NoContent();
            });

        challengeApi.MapDelete("/{challengeId}", async (IChallengeService service, Guid challengeId) =>
        {
            await service.DeleteChallengeAsync(challengeId);
            return TypedResults.NoContent();
        });
        
        challengeApi.MapDelete("/bash/{bashId}", async (IChallengeService service, Guid bashId) =>
        {
            await service.DeleteAllChallengesByBashAsync(bashId);
            return TypedResults.NoContent();
        });
    }
}