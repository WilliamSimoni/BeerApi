using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Dtos
{
    public record QuoteRequestDto
    {
        public int WholesalerId { get; set; }

        public List<QuoteRequestItemDto> Beers { get; set; }
    }

    public record QuoteRequestItemDto
    {
        public int BeerId { get; set; }
        public int Quantity { get; set; }
    }
}
