namespace bach_bash.Models;

public class Submission: EntityBase
{
    public Challenge Challenge { get; private set; }
    
    public Basher Basher { get; private set; }
    
    public Guid ChallengeId { get; private set; }
    
    public Guid BasherId { get; private set; }

    private Submission()
    {
        ChallengeId = Challenge.Id;
        BasherId = Basher.Id;
    }

    private Submission(Guid challengeId, Guid basherId)
    {
        ChallengeId = challengeId;
        BasherId = basherId;
    }

    public static Submission Create(Guid challengeId, Guid basherId)
    {
        ValidateInputs(challengeId, basherId);
        
        return new Submission(challengeId, basherId);
    }
    
    public void UpdateSubmission(Guid challengeId, Guid basherId)
    {
        ValidateInputs(challengeId, basherId);
        
        ChallengeId = challengeId; 
        BasherId = basherId;
        
        UpdateLastModified();
    }
    
    private static void ValidateInputs(Guid challengeId, Guid basherId)
    {
        //check if submission belongs to this challenge by id?
        
        if(Guid.Empty == challengeId)
            throw new ArgumentException("Challenge Id not found",nameof(challengeId));
        if(Guid.Empty == basherId)
            throw new ArgumentException("Basher Id not found",nameof(basherId));

        
    }
    
}