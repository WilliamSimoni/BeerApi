namespace Contracts.Dtos
{
    public record UpdatedInventoryBeerDto
    {
        public int BeerId { get; set; }

        public int Quantity { get; set; }
    }
}
