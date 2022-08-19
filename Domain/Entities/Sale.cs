using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Sale
    {
        public int SaleId { get; set; }

        public DateTime SaleDate { get; set; }
        [Column(TypeName = "Decimal(18,2)")]

        [Range(1, int.MaxValue, ErrorMessage = "The number of unit should be at least {1}")]
        public int NumberOfUnits { get; set; }

        [Column(TypeName = "Decimal(10,2)")]
        [Range(0.01, (double)decimal.MaxValue, ErrorMessage = "The price can not be smaller than {1}")]
        public decimal PricePerUnit { get; set; }

        [Range(0, 100, ErrorMessage = "Discount should be between {1} and {2}")]
        public int Discount { get; set; }

        [Column(TypeName = "Decimal(18,2)")]
        public decimal Total { get; set; }

        public int WholesalerId { get; set; }
        public Wholesaler Wholesaler { get; set; }

        public int BeerId { get; set; }
        public Beer Beer { get; set; }
    }
}
