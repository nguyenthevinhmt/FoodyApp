namespace Foody.Share.Shared.FilterDto
{
    public class PageResultDto<T>
    {
        public IEnumerable<T> Item { get; set; }
        public int TotalItem { get; set; }
    }
}
