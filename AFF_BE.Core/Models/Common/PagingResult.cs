namespace AFF_BE.Core.Models.Common
{
    public class PagingResult<T>
    {
        public PagingResult(List<T>? items, int pageIndex, int pageSize, int totalRecords) 
        { 
            Items = items;

            PageIndex = pageIndex;

            PageSize = pageSize;

            TotalRecords = totalRecords;

            TotalPages = CalculateTotalPages(totalRecords, pageSize);
        }

        public PagingResult(List<T>? items, int pageIndex, int pageSize,string? sortBy, string? orderBy , int totalRecords)
        {
            Items = items;

            PageIndex = pageIndex;

            PageSize = pageSize;

            SortBy = sortBy;

            OrderBy = orderBy;

            TotalRecords = totalRecords;

            TotalPages = CalculateTotalPages(totalRecords, pageSize);
        }

        public List<T>? Items { set; get; }

        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public string? SortBy { get; set; }

        public string? OrderBy { get; set; }

        public int TotalRecords { get; set; }

        public int TotalPages { get; set; }


        private int CalculateTotalPages(int totalRecords, int pageSize)
        {
            return (int)Math.Ceiling(totalRecords / (double)pageSize);
        }
    }
}
