using bach_bash.DTOs.ChallengeDtos;
namespace bach_bash.Services;

public interface IChallengeService
{
    Task<ChallengeDto> CreateChallengeAsync(CreateChallengeDto dto);
    Task<ChallengeDto?> GetChallengeByIdAsync(Guid id);
    Task<IEnumerable<ChallengeDto>> GetAllChallengesByBashAsync(Guid bashId);
    Task UpdateChallengeAsync(Guid id, UpdateChallengeDto dto);
    Task UpdateAllChallengesByBashAsync(Guid bashId, IEnumerable<UpdateChallengeDto> dto);
    Task DeleteChallengeAsync(Guid id);
    Task DeleteAllChallengesByBashAsync(Guid bashId);
}