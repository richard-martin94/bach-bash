using bach_bash.DTOs.SubmissionDtos;
using bach_bash.Models;
using bach_bash.Persistence;
using Microsoft.EntityFrameworkCore;

namespace bach_bash.Services;

public class SubmissionService : ISubmissionService
{
    private readonly BashDbContext _dbContext;
    private readonly ILogger<SubmissionService> _logger;

    public SubmissionService(BashDbContext dbContext, ILogger<SubmissionService> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }
    
    public async Task<SubmissionDto> CreateSubmissionAsync(CreateSubmissionDto command)
    {
        throw new NotImplementedException();
    }

    public async Task<SubmissionDto?> GetSubmissionByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<SubmissionDto?> GetSubmissionsByBashAsync(Guid bashId)
    {
        throw new NotImplementedException();
    }

    public async Task<SubmissionDto?> GetSubmissionByChallengeAsync(Guid challengeId)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<SubmissionDto>> GetAllSubmissionsByChallengeAsync(Guid challengeId)
    {
        throw new NotImplementedException();
    }

    public async Task UpdateSubmissionAsync(Guid id, UpdateSubmissionDto command)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteSubmissionAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}