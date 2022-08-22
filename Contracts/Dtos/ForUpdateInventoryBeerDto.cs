using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Dtos
{
    public record ForUpdateInventoryBeerDto
    {
        [Range(0, int.MaxValue, ErrorMessage = "{0} can not be smaller than {1}")]
        public int Quantity { get; set; }
    }
}
