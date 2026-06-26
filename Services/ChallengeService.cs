using bach_bash.DTOs.ChallengeDtos;
using bach_bash.Models;
using bach_bash.Persistence;
using Microsoft.EntityFrameworkCore;

namespace bach_bash.Services;

public class ChallengeService : IChallengeService
{
    private readonly BashDbContext _dbContext;
    private readonly ILogger<ChallengeService> _logger;

    public ChallengeService(BashDbContext context, ILogger<ChallengeService> logger)
    {
        _dbContext = context;
        _logger = logger;
    }
    
    public async Task<ChallengeDto> CreateChallengeAsync(CreateChallengeDto command)
    {
        var challenge = Challenge.CreateChallenge(command.Title, command.Description, command.Points, command.BashId);
        
        await _dbContext.Challenges.AddAsync(challenge);
        await _dbContext.SaveChangesAsync();

        return new ChallengeDto
        (
            challenge.Id,
            challenge.Title,
            challenge.Description,
            challenge.Points,
            challenge.BashId
        );
    }

    public async Task<ChallengeDto?> GetChallengeByIdAsync(Guid id)
    {
        var challenge = await _dbContext.Challenges
            .AsNoTracking()
            .FirstOrDefaultAsync(c=>c.Id == id);

        if (challenge == null)
            throw new ArgumentNullException($"Challenge id not found");
        return new ChallengeDto
        (
            challenge.Id,
            challenge.Title,
            challenge.Description,
            challenge.Points,
            challenge.BashId
        );
        
    }

    public async Task<IEnumerable<ChallengeDto>> GetAllChallengesByBashAsync(Guid bashId)
    {
        return await _dbContext.Challenges
            .AsNoTracking()
            .Select(challenge => new ChallengeDto(
                challenge.Id,
                challenge.Title,
                challenge.Description,
                challenge.Points,
                challenge.BashId
            )).Where(challenge => challenge.BashId == bashId)
            .ToListAsync();
    }

    public async Task UpdateChallengeAsync(Guid id, UpdateChallengeDto command)
    {
        var challengeToUpdate = await _dbContext.Challenges.FindAsync(id);

        if (challengeToUpdate == null)
            throw new ArgumentNullException($"challenge not found");
        
        challengeToUpdate.UpdateChallenge(command.Id, command.Title, command.Description, command.Points);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAllChallengesByBashAsync(Guid bashId, List<UpdateChallengeDto> dtos)
    {
        var challenges = await _dbContext.Challenges
            .Where(c => c.BashId == bashId)
            .ToListAsync();

        if (challenges == null)
            throw new ArgumentNullException($"Bash has no challenges");

        foreach (var challenge in challenges)
        {
            challenge.UpdateChallenge(challenge.Id, challenge.Title,challenge.Description,challenge.Points);
        }

        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteChallengeAsync(Guid id)
    {
        var challengeToDelete = await _dbContext.Challenges.FindAsync(id);

        if (challengeToDelete == null)
            throw new ArgumentNullException($"Challenge not found");
        
        _dbContext.Challenges.Remove(challengeToDelete);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAllChallengesByBashAsync(Guid bashId)
    {
        
        var challenges = await _dbContext.Challenges
            .Where(c => c.BashId == bashId)
            .ToListAsync();

        if (challenges == null)
            throw new ArgumentNullException($"Bash has no challenges");

        foreach (var challenge in challenges)
        {
            _dbContext.Challenges.Remove(challenge);
        }

        await _dbContext.SaveChangesAsync();
    }
}