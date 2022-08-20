
namespace Contracts.Dtos
{
    public record GetBeerFromSaleDto
    {
        public int BeerId { get; set; }
        public int BreweryId { get; set; }
        public string Name { get; set; } = String.Empty;
        public double AlcoholContent { get; set; }
        public decimal SellingPriceToWholesalers { get; set; }
        public decimal SellingPriceToClients { get; set; }
        //public bool InProduction { get; set; }

    }
}
