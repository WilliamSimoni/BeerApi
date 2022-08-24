namespace Contracts.Dtos
{
    public record CreatedSaleDto
    {
        public int WholesalerId { get; set; }

        public int BeerId { get; set; }

        public int SaleId { get; set; }

        public DateTime SaleDate { get; set; }

        public int NumberOfUnits { get; set; }

        public decimal PricePerUnit { get; set; }

        public int Discount { get; set; }

        public decimal Total { get; set; }
    }
}
