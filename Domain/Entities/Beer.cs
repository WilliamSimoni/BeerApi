using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    [Index(nameof(Name), nameof(OutOfProductionDate), nameof(BreweryId), IsUnique = true)]
    public class Beer
    {
        public int BeerId { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Attribute {0} can have a maximum of {1} characters")]
        public string Name { get; set; } = String.Empty;

        [Range(0.01, 100, ErrorMessage = "Alcohol content must be between {1} and {2}")]
        public double AlcoholContent { get; set; }

        [Column(TypeName = "Decimal(10,2)")]
        [Range(0.01, (double)decimal.MaxValue, ErrorMessage = "The price can not be smaller than {1}")]
        public decimal SellingPriceToWholesalers { get; set; }

        [Column(TypeName = "Decimal(10,2)")]
        [Range(0.01, (double)decimal.MaxValue, ErrorMessage = "The price can not be smaller than {1}")]
        public decimal SellingPriceToClients { get; set; }

        public bool InProduction { get; set; } = true;

        [Required]
        public DateTime OutOfProductionDate { get; set; } = DateTime.MinValue;

        [ForeignKey(nameof(Brewery))]
        public int BreweryId { get; set; }

        public Brewery Brewery { get; set; }

        public ICollection<Sale> Sales { get; set; }
    }
}
