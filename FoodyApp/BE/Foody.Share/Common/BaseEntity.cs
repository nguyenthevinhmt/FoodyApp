namespace Foody.Domain.Common
{
    public class BaseEntity<T>
    {
        public T Id { get; set; }
    }
    public interface ICreated
    {
        public DateTime CreatedAt { get; set; }
        public int CreatedBy { get; set; }
    }
    public interface IUpdated
    {
        public DateTime UpdatedAt { get; set; }
        public int UpdateBy { get; set; }
    }
    public interface ISoftDeleted
    {
        public bool IsDeleted { get; set; }
    }
}

