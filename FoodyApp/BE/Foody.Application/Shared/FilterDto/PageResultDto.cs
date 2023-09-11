namespace Foody.Application.Shared.FilterDto
{
    public class PageResultDto<T>
    {
        public IEnumerable<T> Item { get; set; }
        public int TotalItem { get; set; }
    }
}
