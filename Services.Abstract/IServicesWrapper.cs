using Services.Abstract.UseCaseServices;

namespace Services.Abstract
{
    public interface IServicesWrapper
    {
        public IBreweryBeersCommandServices ChangeBreweryBeers { get; }
        public IBreweryBeersQueryServices QueryBreweryBeers { get; }
        public IBreweryQueryServices QueryBrewery { get; }
        public ISaleQueryServices QuerySale { get; }
        public ISaleCommandServices ChangeSale { get; }
        public IWholesalerQueryServices QueryWholesaler { get; }
        public IWholesalerCommandServices ChangeWholesaler { get; }
    }
}
