using Skinet.Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace Skinet.API.DTO
{
    public class CustomerBasketDto
    {
        [Required]
        public string Id { get; set; }
        [Required]
        public List<BasketItemDto> Items { get; set; } = new List<BasketItemDto>();


    }
}
