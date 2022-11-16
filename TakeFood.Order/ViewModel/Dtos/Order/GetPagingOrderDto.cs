using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace TakeFood.Order.ViewModel.Dtos.Order
{
    public class GetPagingOrderDto
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        [Required]
        public int PageNumber { get; set; }
        [Required]
        public int PageSize { get; set; }
        /// <summary>
        /// Text to search
        /// </summary>
        public String QueryString { get; set; }
        /// <summary>
        /// CreateDate StartDate EndDate Name Code
        /// </summary>
        public String SortBy { get; set; }
        /// <summary>
        /// Desc Asc
        /// </summary>
        public String SortType { get; set; }
    }
}
