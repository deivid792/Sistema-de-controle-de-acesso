namespace VisitorService.Domain.Shared;

public abstract class BaseEntity : Notifiable
{
    public Guid Id { get; private set; }

    protected BaseEntity()
    {
        Id = Guid.NewGuid();
    }

}