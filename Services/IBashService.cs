using bach_bash.DTOs;
namespace bach_bash.Services;

public interface IBashService
{
    Task<BashDto> CreateBashAsync(CreateBashDto dto);
    Task<BashDto?> GetBashByIdAsync(Guid id);
    Task<IEnumerable<BashDto>> GetAllBashesAsync();
    Task UpdateBashAsync(Guid id, UpdateBashDto dto);
    Task DeleteBashAsync(Guid id);
}