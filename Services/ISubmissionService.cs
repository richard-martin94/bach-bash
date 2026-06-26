using bach_bash.DTOs.SubmissionDtos;
namespace bach_bash.Services;

public interface ISubmissionService
{
    Task<SubmissionDto> CreateSubmissionAsync(CreateSubmissionDto dto);
    Task<SubmissionDto?> GetSubmissionByIdAsync(Guid id);
    Task<SubmissionDto?> GetSubmissionByChallengeForMemberAsync(Guid challengeId,Guid basherId);
    Task<IEnumerable<SubmissionDto>> GetAllSubmissionsByChallengeAsync(Guid challengeId);
    Task<IEnumerable<SubmissionDto>> GetAllSubmissionsByMemberAsync(Guid memberId);
    Task UpdateSubmissionAsync(Guid id, UpdateSubmissionDto dto);
    Task UpdateAllSubmissionsByChallengeAsync(Guid challengeId, List<UpdateSubmissionDto> dtos);
    Task DeleteSubmissionAsync(Guid id);
}