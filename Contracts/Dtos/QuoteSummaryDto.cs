using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Dtos
{
    public record QuoteSummaryDto
    {
        public int WholesalerId { get; set; }

        public decimal Total { get; set; }

        public decimal TotalWithoutDiscount { get; set; }

        public decimal AppliedDiscount { get; set; }

        public List<QuoteSummaryItemDto> Beers { get; set; }
    }

    public record QuoteSummaryItemDto
    {
        public int BeerId { get; set; }
        public int RequestedQuantity { get; set; }
        public decimal PricePerUnit { get; set; }
        public decimal SubTotal { get; set; }
    }
}
