namespace bach_bash.Models;

public sealed class Bash : EntityBase
{
    public String Title { get; private set; }
    public int OwnerId { get; private set; }
    
    public ICollection<Challenge> Challenges { get; private set; }

    private Bash()
    {
        Title = "Bash Example Title";
        OwnerId = 0;
        Challenges = new List<Challenge>();
    }

    private Bash(String title, int ownerId)
    {
        Title = title;
        OwnerId = ownerId;
    }

    public static Bash CreateBash(String title, int ownerId)
    {
        ValidateInputs(title, ownerId);
        
        return new Bash(title, ownerId);
    }

    public void UpdateBash(String title, int ownerId)
    {
        ValidateInputs(title, ownerId);

        Title = title;
        
        UpdateLastModified();

    }
    
    private static void ValidateInputs(String title, int ownerId)
    {
        //check if owner owns this bash by id?
        
        if(string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Title can't be empty",nameof(title));
        if(int.IsPositive(ownerId))
            throw new ArgumentException("Owner Id not recognized",nameof(ownerId));
        
    }


}