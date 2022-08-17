namespace Contracts.Dtos
{
    public record BreweryDto
    {
        public int BreweryId { get; set; }
        public string Name { get; set; } = String.Empty;
        public string Address { get; set; } = String.Empty;
        public string Email { get; set; } = String.Empty;
    }
}
