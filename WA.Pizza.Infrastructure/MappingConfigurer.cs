using WA.Pizza.Core.Models;
using WA.Pizza.Core.Models.Items;
using WA.Pizza.Infrastructure.DTO.Basket;
using WA.Pizza.Infrastructure.DTO.Catalog;
using WA.Pizza.Infrastructure.DTO.Order;

namespace WA.Pizza.Infrastructure;

using Mapster;

public static class MappingConfigurer
{
    public static void Configure()
    {
        TypeAdapterConfig<Basket, BasketDTO>.NewConfig()
            .Map(d => d.Id, s => s.Id)
            .Map(d => d.Items, s => s.Items);
        
        TypeAdapterConfig<BasketDTO, Basket>.NewConfig()
            .Map(d => d.Items, s => s.Items);

        TypeAdapterConfig<Order, OrderDTO>.NewConfig()
            .Map(d => d.Id, s => s.Id)
            .Map(d => d.Date, s => s.Date)
            .Map(d => d.Price, s => s.Price)
            .Map(d => d.Items, s => s.Items)
            .Map(d => d.Status, s => s.Status);

        TypeAdapterConfig<OrderDTO, Order>.NewConfig()
           .Map(d => d.Date, s => s.Date)
           .Map(d => d.Price, s => s.Price)
           .Map(d => d.Items, s => s.Items)
           .Map(d => d.Status, s => s.Status);

        TypeAdapterConfig<BasketItem, BasketItemDTO>.NewConfig()
            .Map(d => d.Id, s => s.Id)
            .Map(d => d.Name, s => s.Name)
            .Map(d => d.Price, s => s.Price)
            .Map(d => d.Quantity, s => s.Quantity)
            .Map(d => d.BasketId, s => s.BasketId)
            .Map(d => d.CatalogItemId, s => s.CatalogItemId);

        TypeAdapterConfig<BasketItemDTO, BasketItem>.NewConfig()
            .Map(d => d.Name, s => s.Name)
            .Map(d => d.Price, s => s.Price)
            .Map(d => d.Quantity, s => s.Quantity)
            .Map(d => d.BasketId, s => s.BasketId)
            .Map(d => d.CatalogItemId, s => s.CatalogItemId);

        TypeAdapterConfig<CatalogItem, CatalogItemDTO>.NewConfig()
            .Map(d => d.Id, s => s.Id)
            .Map(d => d.Name, s => s.Name)
            .Map(d => d.Description, s => s.Description)
            .Map(d => d.Price, s => s.Price)
            .Map(d => d.Quantity, s => s.Quantity);

        TypeAdapterConfig<CatalogItemDTO, CatalogItem>.NewConfig()
            .Map(d => d.Name, s => s.Name)
            .Map(d => d.Description, s => s.Description)
            .Map(d => d.Price, s => s.Price)
            .Map(d => d.Quantity, s => s.Quantity);

        TypeAdapterConfig<OrderItem, OrderItemDTO>.NewConfig()
            .Map(d => d.Id, s => s.Id)
            .Map(d => d.Name, s => s.Name)
            .Map(d => d.Price, s => s.Price)
            .Map(d => d.Quantity, s => s.Quantity)
            .Map(d => d.OrderId, s => s.OrderId);

        TypeAdapterConfig<OrderItemDTO, OrderItem>.NewConfig()
            .Map(d => d.Name, s => s.Name)
            .Map(d => d.Price, s => s.Price)
            .Map(d => d.Quantity, s => s.Quantity)
            .Map(d => d.OrderId, s => s.OrderId);

        TypeAdapterConfig<Basket, Order>.NewConfig()
            .Ignore(d => d.Id)
            .Map(d => d.Date, s => DateTime.Now)
            .Map(d => d.Items, s => s.Items)
            .Map(d => d.Status, s => Order.OrderStatus.InProgress);

        TypeAdapterConfig<BasketItem, OrderItem>.NewConfig()
            .Ignore(d => d.Id)
            .Map(d => d.Name, s => s.Name)
            .Map(d => d.Price, s => s.Price)
            .Map(d => d.Quantity, s => s.Quantity);

        TypeAdapterConfig<CatalogItem, BasketItemDTO>.NewConfig()
            .Ignore(d => d.Id)
            .Map(d => d.Name, s => s.Name)
            .Map(d => d.Price, s => s.Price)
            .Map(d => d.CatalogItemId, s => s.Id);
        
        TypeAdapterConfig<AddBasketItemRequest, BasketItem>.NewConfig()
            .Map(d => d.Name, s => s.Name)
            .Map(d => d.Price, s => s.Price)
            .Map(d => d.Quantity, s => s.Quantity)
            .Map(d => d.BasketId, s => s.BasketId)
            .Map(d => d.CatalogItemId, s => s.CatalogItemId);

        TypeAdapterConfig<CreateCatalogItemRequest, CatalogItem>.NewConfig()
            .Map(d => d.Id, s => s.Id)
            .Map(d => d.Name, s => s.Name)
            .Map(d => d.Description, s => s.Description)
            .Map(d => d.Price, s => s.Price)
            .Map(d => d.Quantity, s => s.Quantity);
    }
}