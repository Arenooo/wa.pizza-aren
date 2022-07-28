using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mapster;
using Microsoft.EntityFrameworkCore;
using WA.Pizza.Core.Exceptions;
using WA.Pizza.Core.Models;
using WA.Pizza.Core.Models.Items;
using WA.Pizza.Infrastructure.DTO.Order;

namespace WA.Pizza.Infrastructure.Data.Services
{
    public class OrderDataService
    {
        private PizzaContext _dbContext;

        public OrderDataService(PizzaContext dbContext)
        {
            MappingConfigurer.Configure();
            _dbContext = dbContext;
        }

        public async Task ChangeStatus(int orderId, Order.OrderStatus status)
        {
            var order = _dbContext.Orders.FirstOrDefault(m => m.Id == orderId);

            if (order == null)
                throw new WAPizzaFailedToFindException();

            order.Status = status;

            await _dbContext.SaveChangesAsync();
        }

        public async Task<int> Create(int basketId)
        {
            var basket = await _dbContext.Baskets.Include(m => m.Items).FirstOrDefaultAsync(b => b.Id == basketId);

            if (basket == null)
                throw new WAPizzaFailedToFindException();

            if (basket.Items.Any(i => i.Quantity > i.CatalogItem.Quantity))
                throw new WAPizzaOutOfStockException();
            
            var order = new Order
            {
                Date = DateTime.Now,
                Price = basket.Items.Sum(m => m.Price * m.Quantity),
                Items = basket.Items.Adapt<List<OrderItem>>(),
                Status = Order.OrderStatus.InProgress,
            };

            var o = _dbContext.Orders.Add(order);
            await _dbContext.SaveChangesAsync();

            return o.Entity.Id;
        }

        public async Task<OrderDTO> Get(int id)
        {
            var order = await _dbContext.Orders.FirstOrDefaultAsync(m => m.Id == id);

            if (order == null)
                throw new WAPizzaFailedToFindException();

            return order.Adapt<OrderDTO>();
        }

        public async Task<List<Order>> GetAll() => await _dbContext.Orders.ToListAsync();
    }
}