using System.ComponentModel.DataAnnotations;

namespace Rocket.DTOS
{
    public class BasketItemDTO
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string ProductName { get; set; }
      
        [Required]
        [Range(1,int.MaxValue,ErrorMessage ="Quantity mus be one 1 item at least")]
        public int Quantity { get; set; }
        [Required]
        [Range(0.1, double.MaxValue, ErrorMessage = "Price Must Greater than 0")]
        public decimal Price { get; set; }
        [Required]
        public string PictureURL { get; set; }
        [Required]
        public string Brand { get; set; }
        [Required]
        public string Type { get; set; }
    }
}