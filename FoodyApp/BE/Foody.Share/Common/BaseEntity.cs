namespace Foody.Domain.Common
{
    public class BaseEntity<T>
    {
        public T Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public string CreatedBy { get; set; } = string.Empty;
        public DateTime UpdatedAt { get; set; }
        public string UpdateBy { get; set; } = string.Empty;
        public bool IsDeleted { get; set; }
    }
}
