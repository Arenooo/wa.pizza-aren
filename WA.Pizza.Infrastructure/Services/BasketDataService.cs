using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WA.Pizza.Core.Models.Items;
using WA.Pizza.Core.Models;
using WA.Pizza.Core.Exceptions;

namespace WA.Pizza.Infrastructure.Services
{
    public class BasketDataService
    {
        private PizzaContext _dbContext;

        public BasketDataService(PizzaContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> Create(Basket basket)
        {
            var b = await _dbContext.Baskets.AddAsync(basket);

            if (b == null)
                throw new WAPizzaFailedToCreateException();

            await _dbContext.SaveChangesAsync();

            return b.Entity.Id;
        }

        public async Task<Basket> Get(int id)
        {
            var basket = await _dbContext.Baskets.FirstOrDefaultAsync(m => m.Id == id);
            
            if (basket == null)
                throw new WAPizzaFailedToFindException();

            return basket;
        }
        
        public async Task<List<Basket>> GetAll() => await _dbContext.Baskets.ToListAsync();
        
        public async Task<int> AddToBasket(BasketItem item)
        {
            var basket = await _dbContext.Baskets.FirstOrDefaultAsync(m => m.Id == item.BasketId);

            if (basket == null)
                item.Basket = new Basket();

            _dbContext.BasketItems.Add(item);
            await _dbContext.SaveChangesAsync();

            return item.Id;
        }

        public async Task ClearBasket(int basketId)
        {
            var range = _dbContext.BasketItems.Where(m => m.BasketId == basketId);
            _dbContext.BasketItems.RemoveRange(range);
            await _dbContext.SaveChangesAsync();
        }

        public async Task RemoveFromBasket(int id)
        {
            var item = await _dbContext.BasketItems.FirstOrDefaultAsync(i => i.Id == id);

            if (item == null)
                throw new WAPizzaFailedToFindException();

            _dbContext.BasketItems.Remove(item);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<BasketItem> UpdateItem(BasketItem basketitem)
        {
            var item = _dbContext.BasketItems.First(m => m.Id == basketitem.Id);

            if (item == null)
                throw new WAPizzaFailedToFindException();

            item.Price = basketitem.Price;
            item.Quantity = basketitem.Quantity;
            item.Basket = basketitem.Basket;
            item.CatalogItem = basketitem.CatalogItem;
            item.CatalogItemId = basketitem.CatalogItemId;
            item.Name = basketitem.Name;
            item.BasketId = basketitem.BasketId;

            item = _dbContext.BasketItems.Update(item).Entity;

            await _dbContext.SaveChangesAsync();

            return item;
        }

        public async Task<List<BasketItem>> GetAllItems() => await _dbContext.BasketItems.ToListAsync();

        public Task BindBuyerToBasket()
        {
            throw new NotImplementedException();
        }
    }
}
