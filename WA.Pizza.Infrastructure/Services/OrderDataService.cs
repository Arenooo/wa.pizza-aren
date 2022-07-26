using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WA.Pizza.Core.Exceptions;
using WA.Pizza.Core.Models;
using WA.Pizza.Core.Models.Items;

namespace WA.Pizza.Infrastructure.Services
{
    public class OrderDataService
    {
        private PizzaContext _dbContext;

        public OrderDataService(PizzaContext dbContext)
        {
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
                Status = Order.OrderStatus.InProgress,
            };

            foreach (var basketItem in basket.Items)
            {
                order.Items.Add(new OrderItem
                    {
                        Name = basketItem.CatalogItem.Name,
                        Quantity = basketItem.CatalogItem.Quantity,
                        Price = basketItem.CatalogItem.Price
                    });
            }

            var o = _dbContext.Orders.Add(order);
            await _dbContext.SaveChangesAsync();

            return o.Entity.Id;
        }

        public async Task<Order> Get(int id)
        {
            var order = await _dbContext.Orders.FirstOrDefaultAsync(m => m.Id == id);

            if (order == null)
                throw new WAPizzaFailedToFindException();

            return order;
        }

        public async Task<List<Order>> GetAll() => await _dbContext.Orders.ToListAsync();
    }
}