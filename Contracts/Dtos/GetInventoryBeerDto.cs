namespace Contracts.Dtos
{
    public record GetInventoryBeerDto
    {
        public int BeerId { get; set; }
        public int BreweryId { get; set; }
        public string Name { get; set; } = String.Empty;
        public double AlcoholContent { get; set; }
        public decimal SellingPrice { get; set; }
        public int Quantity { get; set; }
    }
}
