using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    [Index(nameof(BeerId), nameof(WholesalerId), IsUnique = true)]
    public class InventoryBeer
    {
        public int InventoryBeerId { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "{0} cannot be smaller than {1}")]
        public int Quantity { get; set; }
        public int BeerId { get; set; }
        public Beer Beer { get; set; }
        public int WholesalerId { get; set; }
        public Wholesaler Wholesaler { get; set; }
    }
}
