using System.ComponentModel.DataAnnotations;

namespace Contracts.Dtos
{
    public record ForCreationSaleDto
    {
        public int WholesalerId { get; set; }

        public int BeerId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "The number of unit should be at least {1}")]
        public int NumberOfUnits { get; set; }

        [Range(0.01, (double)decimal.MaxValue, ErrorMessage = "The price cannot be smaller than {1}")]
        public decimal PricePerUnit { get; set; }

        [Range(0, 100, ErrorMessage = "Discount should be between {1} and {2}")]
        public int Discount { get; set; }

    }
}
