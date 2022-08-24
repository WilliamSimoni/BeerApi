using System.ComponentModel.DataAnnotations;

namespace Contracts.Dtos
{
    public record ForCreationBeerDto
    {
        [Required]
        [MaxLength(50, ErrorMessage = "Attribute {0} can have a maximum of {1} characters")]
        public string Name { get; set; } = String.Empty;

        [Range(0.01, 100, ErrorMessage = "Alcohol content must be between {1} and {2}")]
        public double AlcoholContent { get; set; }

        [Range(0.01, (double)decimal.MaxValue, ErrorMessage = "Price can not be smaller than {1}")]
        public decimal SellingPriceToWholesalers { get; set; }

        [Range(0.01, (double)decimal.MaxValue, ErrorMessage = "Price can not be smaller than {1}")]
        public decimal SellingPriceToClients { get; set; }
    }
}
