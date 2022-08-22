using Contracts.Dtos;
using Domain.Entities;
using Mapster;

namespace Services.Mappings
{
    public class QuoteMappings : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<(QuoteRequestItemDto quoteReqItem, InventoryBeer inventoryBeer), QuoteSummaryItemDto>()
                .Map(dest => dest.BeerId, src => src.quoteReqItem.BeerId)
                .Map(dest => dest.RequestedQuantity, src => src.quoteReqItem.Quantity)
                .Map(dest => dest.PricePerUnit, src => src.inventoryBeer.Beer.SellingPriceToClients)
                .Map(dest => dest.SubTotal, src => src.inventoryBeer.Beer.SellingPriceToClients * src.quoteReqItem.Quantity);

            config.NewConfig<(QuoteRequestDto quoteRequest, ICollection<QuoteSummaryItemDto> quoteSummaryItems, decimal discount, decimal total), QuoteSummaryDto>()
                .Map(dest => dest.WholesalerId, src => src.quoteRequest.WholesalerId)
                .Map(dest => dest.Total, src => src.total * (100 - src.discount) / 100)
                .Map(dest => dest.TotalWithoutDiscount, src => src.total)
                .Map(dest => dest.AppliedDiscount, src => src.discount)
                .Map(dest => dest.Beers, src => src.quoteSummaryItems);
        }
    }
}