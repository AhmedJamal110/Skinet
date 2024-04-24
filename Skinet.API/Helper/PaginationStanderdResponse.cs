namespace Skinet.API.Helper
{
	public class PaginationStanderdResponse<T>
	{

        public int PageSize { get; set; }
        public int PageIndex { get; set; }
        public int Count { get; set; }
        public IReadOnlyList<T> Data  { get; set; }

        public PaginationStanderdResponse(int pageSize, int pageIndex, int count  , IReadOnlyList<T> data  )
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            Count = count;
            Data = data;

            
        }

    }
}
