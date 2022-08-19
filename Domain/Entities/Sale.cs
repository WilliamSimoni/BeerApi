namespace Domain.Entities
{
    public class Sale
    {
        public int SaleId { get; set; }
        DateTime Date { get; set; }
        public decimal Total { get; set; }
        public int WholesalerId { get; set; }
        public Wholesaler Wholesaler { get; set; }
    }
}