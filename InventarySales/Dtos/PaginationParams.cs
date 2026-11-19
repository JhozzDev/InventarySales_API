using System.ComponentModel.DataAnnotations;

namespace InventarySales.Dtos
{
    public class PaginationParams
    {
        private const int MaxPageSize = 100;
        private int _pageSize = 10;

        [Range(1, int.MaxValue, ErrorMessage = "Page must be greater than 0")]
        public int Page { get; set; } = 1;

        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = Math.Clamp(value, 10, MaxPageSize);
        }
    }
}