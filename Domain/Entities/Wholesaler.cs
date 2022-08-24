using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    [Index(nameof(Name), IsUnique = true)]
    [Index(nameof(Email), IsUnique = true)]
    public class Wholesaler
    {
        public int WholesalerId { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Attribute {0} can have a maximum of {1} characters")]
        public string Name { get; set; } = String.Empty;

        [Required]
        [MaxLength(500, ErrorMessage = "Attribute {0} can have a maximum of {1} characters")]
        public string Address { get; set; } = String.Empty;

        [Required]
        [MaxLength(100, ErrorMessage = "Attribute {0} can have a maximum of {1} characters")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; } = String.Empty;

        public ICollection<Sale> Sales { get; set; }

        public ICollection<InventoryBeer> InventoryBeers { get; set; }
    }
}