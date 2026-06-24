using bach_bash.DTOs.BasherDtos;
namespace bach_bash.Services;

public interface IBasherService
{
    Task<BasherDto> CreateBasherAsync(CreateBasherDto dto);
    Task<BasherDto?> GetBasherByIdAsync(Guid id);
    Task<IEnumerable<BasherDto>> GetAllBashersAsync();
    Task<IEnumerable<BasherDto>> GetAllBashersByBashAsync(Guid bashId);
    Task<IEnumerable<BasherDto>> GetAllBashersByChallengeAsync(Guid challengeId);
    Task UpdateBasherAsync(Guid id, UpdateBasherDto dto);
    Task DeleteBasherAsync(Guid id);
}