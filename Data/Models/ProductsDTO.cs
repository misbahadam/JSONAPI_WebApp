using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace JSONAPI_WebApp.Data.Models
{
    public class ProductDTO
    {
        [Required]
        public required string ProductId { get; set; }
        public string? ProductName { get; set; }
        public int StockAvailable { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
