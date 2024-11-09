namespace AFF_BE.Core.Models.Common
{
    public class PagingRequest
    {
        public int PageIndex { get; set; } = 1;

        public int PageSize { get; set; } =10;

        public string? SortBy { get; set; }

        public string? OrderBy { get; set; }
    }
}
