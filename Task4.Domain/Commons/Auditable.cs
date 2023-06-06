namespace Task4.Domain.Commons;
public abstract class Auditable
{
    public long Id { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? LastLoginTime { get; set; }

}
