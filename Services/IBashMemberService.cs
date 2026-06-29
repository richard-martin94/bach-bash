using bach_bash.DTOs.BashMemberDtos;
using bach_bash.Models;

namespace bach_bash.Services;

public interface IBashMemberService
{
    Task<BashMemberDto> CreateBashMemberAsync(CreateBashMemberDto dto);
    Task<IEnumerable<BashMemberDto?>> GetBashesByMemberIdAsync(Guid id);
    Task<IEnumerable<BashMemberDto>> GetAllBashMembersByBashAsync(Guid bashId);
    //delete all members from bash
    Task DeleteAllBashMembersByBashAsync(Guid bashId);
    //delete basher from all bashes
    Task DeleteBasherFromAllBashesAsync(Guid id);
    //delete basher from one bash
    Task DeleteBashMemberFromOneBashAsync(Guid id, Guid bashId);
}