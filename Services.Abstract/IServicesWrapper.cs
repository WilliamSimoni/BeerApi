using Services.Abstract.UseCaseServices;

namespace Services.Abstract
{
    public interface IServicesWrapper
    {
        public ICommandBreweryBeersServices ChangeBreweryBeers { get; }
        public IQueryBreweryBeersServices QueryBreweryBeers { get; }
        public IQueryBreweryServices QueryBrewery { get; }
    }
}
