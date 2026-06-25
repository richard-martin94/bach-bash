namespace bach_bash.Models;

public sealed class Bash : EntityBase
{
    public String Title { get; private set; }
    public Guid OwnerId { get; private set; }
    
    public Basher Owner { get; private set; }
    
    public ICollection<Challenge> Challenges { get; private set; }
    
    public ICollection<BashMember> BashMmebers { get; private set; }

    private Bash()
    {
        Title = string.Empty;
        OwnerId = Guid.Empty;
    }

    private Bash(String title, Guid ownerId)
    {
        Title = title;
        OwnerId = ownerId;
    }

    public static Bash CreateBash(String title, Guid ownerId)
    {
        ValidateInputs(title, ownerId);
        
        return new Bash(title, ownerId);
    }

    public void UpdateBash(String title, Guid ownerId)
    {
        ValidateInputs(title, ownerId);

        Title = title;
        
        UpdateLastModified();

    }
    
    private static void ValidateInputs(String title, Guid ownerId)
    {
        //check if owner owns this bash by id?
        
        if(string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Title can't be empty",nameof(title));
        if(Guid.Empty == ownerId)
            throw new ArgumentException("Owner Id not recognized",nameof(ownerId));
        
    }


}