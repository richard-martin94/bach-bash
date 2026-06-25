using bach_bash.DTOs.SubmissionDtos;
namespace bach_bash.Services;

public interface ISubmissionService
{
    Task<SubmissionDto> CreateSubmissionAsync(CreateSubmissionDto dto);
    Task<SubmissionDto?> GetSubmissionByIdAsync(Guid id);
    Task<SubmissionDto?> GetSubmissionsByBashAsync(Guid bashId);
    Task<SubmissionDto?> GetSubmissionByChallengeAsync(Guid challengeId);
    Task<IEnumerable<SubmissionDto>> GetAllSubmissionsByChallengeAsync(Guid challengeId);
    Task UpdateSubmissionAsync(Guid id, UpdateSubmissionDto dto);
    Task DeleteSubmissionAsync(Guid id);
}