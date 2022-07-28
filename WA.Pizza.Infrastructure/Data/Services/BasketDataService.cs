using System.Data.Entity;
using Mapster;
using WA.Pizza.Core.Models.Items;
using WA.Pizza.Core.Models;
using WA.Pizza.Core.Exceptions;
using WA.Pizza.Infrastructure.DTO.Basket;

namespace WA.Pizza.Infrastructure.Data.Services
{
    public class BasketDataService
    {
        private PizzaContext _dbContext;

        public BasketDataService(PizzaContext dbContext)
        {
            MappingConfigurer.Configure();
            _dbContext = dbContext;
        }

        public async Task<BasketDTO> Get(int basketId)
        {
            var basket = await _dbContext.Baskets.Include(i => i.Items).ProjectToType<BasketDTO>().FirstOrDefaultAsync(m => m.Id == basketId);
            
            if (basket == null)
                throw new WAPizzaFailedToFindException();

            return basket;
        }

        public async Task<int> AddToBasket(AddBasketItemRequest request)
        {
            var basketItem = request.Adapt<BasketItem>();
            
            if (!_dbContext.Baskets.Any(m => m.Id == request.BasketId))
                basketItem.Basket = new Basket();

            _dbContext.BasketItems.Add(basketItem);
            await _dbContext.SaveChangesAsync();

            return request.Id;
        }

        public async Task ClearBasket(int basketId)
        {
            var range = _dbContext.BasketItems.Where(m => m.BasketId == basketId);
            _dbContext.BasketItems.RemoveRange(range);
            await _dbContext.SaveChangesAsync();
        }

        public async Task RemoveFromBasket(int itemId)
        {
            var item = await _dbContext.BasketItems.FirstOrDefaultAsync(i => i.Id == itemId);

            if (item == null)
                throw new WAPizzaFailedToFindException();

            _dbContext.BasketItems.Remove(item);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<int> UpdateItem(BasketItem request)
        {
            var item = await _dbContext.BasketItems.FirstOrDefaultAsync(m => m.Id == request.Id);

            if (item == null)
                throw new WAPizzaFailedToFindException();

            item.Quantity = request.Quantity;

            await _dbContext.SaveChangesAsync();

            return item.Id;
        }

        public Task BindBuyerToBasket()
        {
            throw new NotImplementedException();
        }
    }
}
