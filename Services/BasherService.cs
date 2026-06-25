using bach_bash.DTOs.BashDtos;
using bach_bash.DTOs.BasherDtos;
using bach_bash.Models;
using bach_bash.Persistence;
using Microsoft.EntityFrameworkCore;

namespace bach_bash.Services;

public class BasherService : IBasherService
{
    private readonly BashDbContext _dbContext;
    private readonly ILogger<BasherService> _logger;

    public BasherService(BashDbContext dbContext, ILogger<BasherService> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<BasherDto> CreateBasherAsync(CreateBasherDto command)
    {
        var basher = Basher.CreateBasher(command.Username);
        
        await _dbContext.Bashers.AddAsync(basher);
        await _dbContext.SaveChangesAsync();

        return new BasherDto
        (
            basher.Id,
            basher.Username
        );
    }

    public async Task<BasherDto?> GetBasherByIdAsync(Guid id)
    {
        var basher = await _dbContext.Bashers
            .AsNoTracking()
            .FirstOrDefaultAsync(b => b.Id == id);
        if (basher is null)
            throw new ArgumentNullException($"Basher not found");
        return new BasherDto
        (
            basher.Id,
            basher.Username
        );
    }

    public async Task <IEnumerable<BasherDto>> GetAllBashersAsync()
    {
        return await _dbContext.Bashers
            .AsNoTracking()
            .Select(basher => new BasherDto(
                basher.Id,
                basher.Username))
            .ToListAsync();
    }

    public async Task<IEnumerable<BasherDto>> GetAllBashersByBashAsync(Guid bashId)
    {
        //move to new junction table bash x bashers to define members and add owner flag
        throw new NotImplementedException();
    }

    public async Task UpdateBasherAsync(Guid id, UpdateBasherDto command)
    {
        var basherToUpdate = await _dbContext.Bashers.FindAsync(id);
        
        if(basherToUpdate is null || command is null)
            throw new ArgumentNullException($"Invalid basher id or command dto");
        basherToUpdate.UpdateBasher(command.Username);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteBasherAsync(Guid id)
    {
        var basherToDelete = await _dbContext.Bashers.FindAsync(id);
        
        if(basherToDelete is null)
            throw new ArgumentNullException($"Invalid basher id or basher id not found");
        
        _dbContext.Bashers.Remove(basherToDelete);
        await _dbContext.SaveChangesAsync();
    }
    
}