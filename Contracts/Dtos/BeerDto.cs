using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Dtos
{
    public record BeerDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = String.Empty;

        [Display(Name = "Alcohol content")]
        public double AlcoholContent { get; set; }

        [Display(Name = "Price for wholesalers")]
        public double SellingPriceToWholesalers { get; set; }

        [Display(Name = "Price for clients")]
        public double SellingPriceToClients { get; set; }
    }
}
