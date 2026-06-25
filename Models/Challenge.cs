namespace bach_bash.Models;

public sealed class Challenge : EntityBase
{
    public String Title { get; private set; }
    public String Description { get; private set; }
    public int Points { get; private set; }
    public Guid BashId { get; private set; }
    public Bash Bash { get; private set; } = null!;

    public ICollection<Submission> Submissions { get; private set; }
    

    private Challenge()
    {
        Title = "Example challenge title";
        Description = "Example challenge description";
        Points = 10;
        BashId = Bash.Id;
        Submissions = new List<Submission>();
    }

    private Challenge(String title, String description, int points, Guid bashId)
    {
        Title = title;
        Description = description;
        Points = points;
        BashId = bashId;
    }

    public static Challenge CreateChallenge(String title, String description, int points, Guid bashId)
    {
        ValidateInputs(title, description, points, bashId);
        
        return new Challenge(title, description, points, bashId);
    }

    public void UpdateChallenge(Guid id, String title, String description, int points)
    {
        ValidateInputs(Id, title, description, points);

        Title = title;
        Description = description;
        Points = points;
        
        UpdateLastModified();
    }

    private static void ValidateInputs(Guid id, string title, string description, int points)
    {
        if(string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Title can't be empty",nameof(title));
        if(string.IsNullOrWhiteSpace(description))
            throw new ArgumentException("Description can't be empty",nameof(description));
        if(points < 1)
            throw new ArgumentException("Points can't be negative or zero",nameof(points));
        if(Guid.Empty == id)
            throw new ArgumentException("bash Id not found",nameof(id));
    }
    private static void ValidateInputs(String title, string description, int points, Guid bashId)
    {
        //check if challenge belongs to this bash by id?
        
        if(string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Title can't be empty",nameof(title));
        if(string.IsNullOrWhiteSpace(description))
            throw new ArgumentException("Description can't be empty",nameof(description));
        if(points < 1)
            throw new ArgumentException("Points can't be negative or zero",nameof(points));
        if(Guid.Empty == bashId)
            throw new ArgumentException("bash Id not found",nameof(bashId));
        
    }
}