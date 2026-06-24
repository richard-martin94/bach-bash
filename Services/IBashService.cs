using bach_bash.DTOs.BashDtos;
namespace bach_bash.Services;

public interface IBashService
{
    Task<BashDto> CreateBashAsync(CreateBashDto dto);
    Task<BashDto?> GetBashByIdAsync(Guid id);
    Task<IEnumerable<BashDto>> GetAllBashesAsync();
    Task<IEnumerable<BashDto>> GetAllBashesByOwnerAsync(Guid ownerId);
    Task UpdateBashAsync(Guid id, UpdateBashDto dto);
    Task DeleteBashAsync(Guid id);
}