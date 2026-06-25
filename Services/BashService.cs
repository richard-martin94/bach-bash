using bach_bash.DTOs.BashDtos;
using bach_bash.Models;
using bach_bash.Persistence;
using Microsoft.EntityFrameworkCore;

namespace bach_bash.Services;

public class BashService : IBashService
{
    private readonly BashDbContext _dbContext;
    private readonly ILogger<BashService> _logger;

    public BashService(BashDbContext dbContext, ILogger<BashService> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<BashDto> CreateBashAsync(CreateBashDto command)
    {
        var bash = Bash.CreateBash(command.Title, command.OwnerId);

        await _dbContext.Bashes.AddAsync(bash);
        await _dbContext.SaveChangesAsync();

        return new BashDto
        (
            bash.Id,
            bash.Title,
            bash.OwnerId
        );

    }

    public async Task<BashDto?> GetBashByIdAsync(Guid id)
    {
        var bash = await _dbContext.Bashes
            .AsNoTracking()
            .FirstOrDefaultAsync(b => b.Id == id);

        if (bash is null)
            return null;

        return new BashDto
        (
            bash.Id,
            bash.Title,
            bash.OwnerId
        );
    }

    public async Task<IEnumerable<BashDto>> GetAllBashesAsync()
    {
        return await _dbContext.Bashes
            .AsNoTracking()
            .Select(bash => new BashDto(
                bash.Id,
                bash.Title,
                bash.OwnerId
                ))
            .ToListAsync();
    }

    public async Task<IEnumerable<BashDto>> GetAllBashesByOwnerAsync(Guid ownerId)
    {
        return await _dbContext.Bashes
            .AsNoTracking()
            .Select(bash => new BashDto(
                bash.Id,
                bash.Title,
                bash.OwnerId
            )).Where(bash => bash.OwnerId == ownerId)
            .ToListAsync();
    }

    public async Task UpdateBashAsync(Guid id, UpdateBashDto command)
    {
        var bashToUpdate = await _dbContext.Bashes.FindAsync(id);
        
        if (bashToUpdate is null || command is null)
            throw new ArgumentNullException($"Invalid BashId or DTO command");
        
        bashToUpdate.UpdateBash(command.Title, command.OwnerId);
        await _dbContext.SaveChangesAsync();
    }
    public async Task DeleteBashAsync(Guid id)
    {
        var bashToDelete = await _dbContext.Bashes.FindAsync(id);
        if (bashToDelete is null)
            throw new ArgumentNullException($"Invalid BashId");
        
        _dbContext.Bashes.Remove(bashToDelete);
        await _dbContext.SaveChangesAsync();
    }

}