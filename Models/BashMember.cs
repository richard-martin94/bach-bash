namespace bach_bash.Models;

public class BashMember : EntityBase
{
    public Bash Bash { get; private set; }
    public Basher Basher { get; private set; }
    public Guid BashId { get; private set; }
    public Guid BasherId { get; private set; }

    private BashMember()
    {
        BashId = Guid.Empty;
        BasherId = Guid.Empty;
    }

    private BashMember(Guid bashId, Guid basherId)
    {
        BashId = bashId;
        BasherId = basherId;
    }

    public static BashMember CreateBashMember(Guid bashId, Guid basherId)
    {
        ValidateInputs(bashId, basherId);

        return new BashMember(bashId, basherId);
    }
    
    private static void ValidateInputs(Guid bashId, Guid basherId)
    {
        if(Guid.Empty == bashId)
            throw new ArgumentException("Bash Id not found",nameof(bashId));
        if(Guid.Empty == basherId)
            throw new ArgumentException("Basher Id not found",nameof(basherId));
    }
}