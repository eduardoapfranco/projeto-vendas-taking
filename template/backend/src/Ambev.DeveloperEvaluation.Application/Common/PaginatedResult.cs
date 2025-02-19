namespace Ambev.DeveloperEvaluation.Application.Common
{
    public class PaginatedResult<T>
    {
        public List<T> Items { get; set; } = new List<T>();
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int TotalCount { get; set; }

        public bool HasPrevious => CurrentPage > 1;
        public bool HasNext => CurrentPage < TotalPages;

        public PaginatedResult() { }

        public PaginatedResult(List<T> items, int count, int pageNumber, int pageSize)
        {
            Items = items;
            TotalCount = count;
            CurrentPage = pageNumber;
            TotalPages = (int)System.Math.Ceiling(count / (double)pageSize);
        }
    }
}
