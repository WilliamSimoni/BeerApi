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

        [Range(0.01, double.MaxValue, ErrorMessage = "Price can not be smaller than {1}")]
        [RegularExpression(@"^\d+(\.\d{2})?$", ErrorMessage = "Price should be an integer or a real number with two decimals. I.e.: both 2 or 2.99 are valid prices")]
        public double SellingPriceToWholesalers { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Price can not be smaller than {1}")]
        [RegularExpression(@"^\d+(\.\d{2})?$", ErrorMessage = "Price should be an integer or a real number with two decimals. I.e.: both 2 or 2.99 are valid prices")]
        public double SellingPriceToClients { get; set; }
    }
}
