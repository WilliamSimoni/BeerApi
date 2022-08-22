using System.ComponentModel.DataAnnotations;

namespace Contracts.Dtos
{
    public record QuoteRequestDto
    {
        public int WholesalerId { get; set; }

        [Required, MinLength(1)]
        [NotDuplicatedBeers]
        public ICollection<QuoteRequestItemDto> Beers { get; set; }
    }

    public record QuoteRequestItemDto
    {
        public int BeerId { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "{0} can not be smaller than {1}")]
        public int Quantity { get; set; }
    }

    public sealed class NotDuplicatedBeers : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            var beers = value as IEnumerable<QuoteRequestItemDto>;

            if (beers is null)
                return false;

            var beerIds = beers.Select(b => b.BeerId).ToList();

            return beerIds.Distinct().Count() == beers.Count();
        }

        public override string FormatErrorMessage(string name)
        {
            return "Duplicate beers in the order";
        }
    }
}
