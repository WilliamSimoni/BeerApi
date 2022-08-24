using System.ComponentModel.DataAnnotations;

namespace Contracts.Dtos
{
    public record BeerDto
    {
        public int BeerId { get; set; }

        public string Name { get; set; } = String.Empty;

        [Display(Name = "Alcohol content")]
        public double AlcoholContent { get; set; }

        [Display(Name = "Price for wholesalers")]
        public decimal SellingPriceToWholesalers { get; set; }

        [Display(Name = "Price for clients")]
        public decimal SellingPriceToClients { get; set; }
    }
}
