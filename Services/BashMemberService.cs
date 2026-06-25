using bach_bash.DTOs.BashMemberDtos;
using bach_bash.Models;
using bach_bash.Persistence;
using Microsoft.EntityFrameworkCore;

namespace bach_bash.Services;

public class BashMemberService : IBashMemberService
{
    private readonly BashDbContext _dbContext;
    private readonly ILogger<BashMemberService> _logger;

    public BashMemberService(BashDbContext dbContext, ILogger<BashMemberService> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<BashMemberDto> CreateBashMemberAsync(CreateBashMemberDto command)
    {
        var bashMember = BashMember.CreateBashMember(command.BashId, command.BasherId);
        
        await _dbContext.BashMembers.AddAsync(bashMember);
        await _dbContext.SaveChangesAsync();
        
        return new BashMemberDto
        (
            bashMember.Id, 
            bashMember.BashId
        );
    }

    //all bashes this member participates in
    public async Task<IEnumerable<BashMemberDto?>> GetBashMembersByIdAsync(Guid id)
    {
        return await _dbContext.BashMembers
            .Select(bashmember => new BashMemberDto(bashmember.Id, bashmember.BashId))
            .Where(m => m.BasherId == id)
            .ToListAsync();
    }

    //all bash members in a bash
    public async Task<IEnumerable<BashMemberDto>> GetAllBashMembersByBashAsync(Guid bashId)
    {
        return await _dbContext.BashMembers
            .Select(bashmember => new BashMemberDto(bashmember.Id, bashmember.BashId))
            .Where(b => b.BashId == bashId)
            .ToListAsync();
    }

    //delete all members from bash
    public async Task DeleteAllBashMembersByBashAsync(Guid bashId)
    { 
        var bashMembers = await _dbContext.BashMembers
            .Where(bm =>bm.BashId == bashId)
            .ToListAsync();

        if (bashMembers is null)
            throw new ArgumentNullException($"bash has no members");

        foreach (var basher in bashMembers)
        {
            _dbContext.BashMembers.Remove(basher);
        }
        await _dbContext.SaveChangesAsync();
    }
    
    //delete basher from all bashes
    public async Task DeleteBasherFromAllBashesAsync(Guid id)
    {
        var bashes = await _dbContext.BashMembers
            .Where(bm => bm.BasherId == id)
            .ToListAsync();

        if (bashes is null)
            throw new ArgumentNullException($"bashes not found or basher does not exist");

        foreach (var bash in bashes)
        {
            _dbContext.BashMembers.Remove(bash);
        }
        await _dbContext.SaveChangesAsync();
    }
    
    //delete basher from one bash
    public async Task DeleteBashMemberFromOneBashAsync(Guid id, Guid bashId)
    {
        var bash = await _dbContext.BashMembers
            .Where(bm =>(bm.BashId == bashId) && (bm.BasherId == id))
            .FirstOrDefaultAsync();

        if (bash is null)
            throw new ArgumentNullException($"bash not found or basher does not exist");

        _dbContext.BashMembers.Remove(bash);
        
        await _dbContext.SaveChangesAsync();
    }
    
}