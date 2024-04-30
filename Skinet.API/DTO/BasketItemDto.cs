using System.ComponentModel.DataAnnotations;

namespace Skinet.API.DTO
{
    public class BasketItemDto
    {
        [Required]
        public int Id { get; set; }
        [Required]

        public string name { get; set; }
        [Required]

        public string PictureUrl { get; set; }
        [Required]

        public string Brand { get; set; }
        [Required]

        public string Type { get; set; }
        [Required]
        [Range(1 , int.MaxValue , ErrorMessage ="Quntity must be one at least")]

        public int Quntity { get; set; }
        [Required]

        [Range( 0.1 , double.MaxValue , ErrorMessage ="Price must be more than zero" )]
        public decimal price { get; set; }

    }
}
