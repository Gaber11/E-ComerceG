namespace Shared.Dtos
{
    public record BasketDto
    {
        public string Id { get; init; }
        public IEnumerable<BasketItemDto> Items { get; init; }

        public int? DeliveryMethodId { get; init; }
        public decimal? ShippingPrice { get; init; }

        public string? PaymentIntentId { get; init; }
        public string? ClientSecret { get; init; }
    }
}
