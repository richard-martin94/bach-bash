using bach_bash.DTOs.SubmissionDtos;
using bach_bash.Services;

namespace bach_bash.Endpoints;

public static class SubmissionEndpoints
{
    public static void MapSubmissionEndpoints(this IEndpointRouteBuilder routes)
    {
        var submissionApi = routes.MapGroup("/api/submissions").WithTags("Submissions");
        
        submissionApi.MapPost("/", async (ISubmissionService service, CreateSubmissionDto command) =>
        {
            var submission = await service.CreateSubmissionAsync(command);
            return TypedResults.Created($"/api/submissions/{submission.ChallengeId}/{submission.BasherId}", submission);
        });
        
        submissionApi.MapGet("/{submissionId}", async (ISubmissionService service, Guid submissionId) =>
        {
            var submission = await service.GetSubmissionByIdAsync(submissionId);
            return submission is null
                ? (IResult)TypedResults.NotFound(new { Message = $"submission with ID {submissionId} not found." })
                : TypedResults.Ok(submission);
        });
        
        submissionApi.MapGet("/challenge/{challengeId}", async (ISubmissionService service, Guid challengeId) =>
        {
            var submissions = await service.GetAllSubmissionsByChallengeAsync(challengeId);
            return submissions is null
                ? (IResult)TypedResults.NotFound(new { Message = $"submissions from challenge ID {challengeId} not found." })
                : TypedResults.Ok(submissions);
        });
        
        submissionApi.MapGet("/basher/{basherId}", async (ISubmissionService service, Guid basherId) =>
        {
            var submissions = await service.GetAllSubmissionsByMemberAsync(basherId);
            return submissions is null
                ? (IResult)TypedResults.NotFound(new { Message = $"submissions for {basherId} not found." })
                : TypedResults.Ok(submissions);
        });
        
        submissionApi.MapGet("/challenge-basher/{challengeId}/{basherId}", async (ISubmissionService service, Guid challengeId, Guid basherId) =>
        {
            var submissions = await service.GetSubmissionByChallengeForMemberAsync(challengeId, basherId);
            return submissions is null
                ? (IResult)TypedResults.NotFound(new { Message = $"submissions from challenge ID {challengeId} for basher {basherId} not found." })
                : TypedResults.Ok(submissions);
        });
        
        submissionApi.MapPut("/{submissionId}", async (ISubmissionService service, Guid submissionId, UpdateSubmissionDto command) =>
        {
            await service.UpdateSubmissionAsync(submissionId, command);
            return TypedResults.NoContent();
        });
        
        submissionApi.MapPut("/challenge/{challengeId}", async (ISubmissionService service, Guid challengeId, List<UpdateSubmissionDto> command) =>
        {
            await service.UpdateAllSubmissionsByChallengeAsync(challengeId, command);
            return TypedResults.NoContent();
        });

        submissionApi.MapDelete("/{submissionId}", async (ISubmissionService service, Guid submissionId) =>
        {
            await service.DeleteSubmissionAsync(submissionId);
            return TypedResults.NoContent();
        });

    }
}