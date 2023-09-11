namespace Foody.Domain.Common
{
    public class BaseEntity<T>
    {
        public T Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime UpdateBy { get; set; }
        public bool IsDeleted { get; set; }
    }
}
