namespace bach_bash.Models;

public class Submission: EntityBase
{
    public Challenge Challenge { get; private set; }
    public Basher Basher { get; private set; }
    public Guid ChallengeId { get; private set; }
    public Guid BasherId { get; private set; }
    public int Place { get; private set; }

    private Submission()
    {
        ChallengeId = Guid.Empty;
        BasherId = Guid.Empty;
        Place = 0;
    }

    private Submission(Guid challengeId, Guid basherId, int place)
    {
        ChallengeId = challengeId;
        BasherId = basherId;
        Place = place;
    }

    public static Submission CreateSubmission(Guid challengeId, Guid basherId, int place)
    {
        ValidateInputs(challengeId, basherId, place);
        
        return new Submission(challengeId, basherId, place);
    }
    
    public void UpdateSubmission(Guid challengeId, Guid basherId, int place)
    {
        ValidateInputs(challengeId, basherId, place);
        
        ChallengeId = challengeId; 
        BasherId = basherId;
        Place = place;
        
        UpdateLastModified();
    }
    
    private static void ValidateInputs(Guid challengeId, Guid basherId, int place)
    {
        //check if submission belongs to this challenge by id?
        
        if(Guid.Empty == challengeId)
            throw new ArgumentException("Challenge Id not found",nameof(challengeId));
        if(Guid.Empty == basherId)
            throw new ArgumentException("Basher Id not found",nameof(basherId));
        if(place > 4)
            throw new ArgumentException("Place must be less than or equal to 4", nameof(place));
        
    }
    
}