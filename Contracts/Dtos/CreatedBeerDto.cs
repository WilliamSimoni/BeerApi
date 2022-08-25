using System.ComponentModel.DataAnnotations;

namespace Contracts.Dtos
{
    public record CreatedBeerDto
    {
        public int BeerId { get; set; }

        public string Name { get; set; } = String.Empty;

        public double AlcoholContent { get; set; }

        public decimal SellingPriceToWholesalers { get; set; }

        public decimal SellingPriceToClients { get; set; }
    }
}
