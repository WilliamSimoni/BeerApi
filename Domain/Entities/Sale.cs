using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Sale
    {
        public int SaleId { get; set; }
        public DateTime SaleDate { get; set; }
        [Column(TypeName = "Decimal(18,2)")]
        public decimal Total { get; set; }
        public int WholesalerId { get; set; }
        public Wholesaler Wholesaler { get; set; }
    }
}