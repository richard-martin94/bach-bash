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
        var submission = Submission.CreateSubmission(command.ChallengeId, command.BasherId, command.Place);
        
        await _dbContext.Submissions.AddAsync(submission);
        await _dbContext.SaveChangesAsync();

        return new SubmissionDto
            (
                submission.ChallengeId,
                submission.BasherId,
                submission.Place
            );
    }

    public async Task<SubmissionDto?> GetSubmissionByIdAsync(Guid id)
    {
        var submission = await _dbContext.Submissions
            .AsNoTracking()
            .FirstOrDefaultAsync(s => s.Id == id);
        if (submission == null)
            throw new ArgumentNullException($"submission not found");
        return new SubmissionDto
        (
            submission.ChallengeId,
            submission.BasherId,
            submission.Place
        );
    }

    public async Task<SubmissionDto?> GetSubmissionByChallengeForMemberAsync(Guid challengeId, Guid basherId)
    {
        var submission = await _dbContext.Submissions
            .Where(s => (s.ChallengeId == challengeId) && (s.BasherId == basherId))
            .FirstOrDefaultAsync();
        if (submission == null)
            throw new ArgumentNullException($"submission not found for basher or challenge");
        return new SubmissionDto
        (
            submission.ChallengeId,
            submission.BasherId,
            submission.Place
        );
    }

    public async Task<IEnumerable<SubmissionDto>> GetAllSubmissionsByChallengeAsync(Guid challengeId)
    {
        var submissions =  await _dbContext.Submissions 
            .AsNoTracking()
            .Select(submission => new SubmissionDto
                (
                    submission.ChallengeId, 
                    submission.BasherId, 
                    submission.Place
                ))
            .Where(c => c.ChallengeId == challengeId)
            .OrderBy(s => s.Place)
            .ToListAsync();
        return submissions;
    }

    public async Task<IEnumerable<SubmissionDto>> GetAllSubmissionsByMemberAsync(Guid basherId)
    {
        return await _dbContext.Submissions 
        .AsNoTracking()
        .Select(submission => new SubmissionDto
        (
            submission.ChallengeId, 
            submission.BasherId, 
            submission.Place
        ))
        .Where(b => b.BasherId == basherId)
        .ToListAsync();
    }
    
    public async Task UpdateSubmissionAsync(Guid id, UpdateSubmissionDto command)
    {
        var submissionToUpdate = await _dbContext.Submissions.FindAsync(id);

        if (submissionToUpdate == null)
            throw new ArgumentNullException($"Submission not found");
        
        submissionToUpdate.UpdateSubmission(command.ChallengeId, command.BasherId, command.Place);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAllSubmissionsByChallengeAsync(Guid challengeId, List<UpdateSubmissionDto> dtos)
    {
        var submissions = await _dbContext.Submissions
            .Where(s => s.ChallengeId == challengeId)
            .OrderBy(s => s.Place)
            .ToListAsync();

        if (submissions == null)
            throw new ArgumentNullException($"Challenge has no submissions");
        
        var i = 0;
        foreach (var submission in submissions)
        {
            if (dtos[i].BasherId != Guid.Empty && dtos[i].Place == i)
            {
                submission.UpdateSubmission(dtos[i].ChallengeId, dtos[i].BasherId, dtos[i].Place);
                ++i;   
            }
            else
            {
                break;
            }
        }

        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteSubmissionAsync(Guid id)
    {
        var submissionToDelete = await _dbContext.Submissions.FindAsync(id);

        if (submissionToDelete != null)
        {
            _dbContext.Submissions.Remove(submissionToDelete);
            await _dbContext.SaveChangesAsync();
        } 
    }
}