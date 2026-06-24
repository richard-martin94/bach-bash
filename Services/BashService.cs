using bach_bash.DTOs.BashDtos;
using bach_bash.Models;
using bach_bash.Persistence;

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

        List<Challenge> challenges = new List<Challenge>();

        return new BashDto
        (
            bash.Id,
            bash.Title,
            bash.OwnerId,
            challenges
        );

    }

    public async Task<BashDto?> GetBashByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<BashDto>> GetAllBashesAsync()
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<BashDto>> GetAllBashesByOwnerAsync(Guid ownerId)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteBashAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateBashAsync(Guid id, UpdateBashDto command)
    {
        throw new NotImplementedException();
    }
}