namespace Core.Entities.Common
{
    public interface ISoftDeletable
    {
        bool IsDeleted { get; set; }

    }
}
