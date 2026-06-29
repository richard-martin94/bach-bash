namespace bach_bash.Models;

public class Basher : EntityBase
{
    public String Username { get; private set; }
    
    // Foreign key pointing to bash
    public Guid OwnerId { get; set; }

    // Navigation back to bash
    public Bash Bash { get; set; } = null!;
    
    public ICollection<Submission> Submissions { get; private set; } 

    private Basher()
    {
        Username = "Example Username";
        Submissions = new List<Submission>();
    }

    private Basher(String username)
    {
        Username = username;
    }

    public static Basher CreateBasher(String username)
    {
        ValidateInputs(username);

        return new Basher(username);
    }

    public void UpdateBasher(String username)
    {
        ValidateInputs(username);

        Username = username;
        
        UpdateLastModified();
    }
    
    private static void ValidateInputs(String username)
    {
        //check if challenge belongs to this bash by id?

        if (string.IsNullOrWhiteSpace(username))
            throw new ArgumentException("Username can't be empty", nameof(username));

    }
}