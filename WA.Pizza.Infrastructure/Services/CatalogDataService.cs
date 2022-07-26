using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WA.Pizza.Core.Models;
using WA.Pizza.Core.Models.Items;
using WA.Pizza.Core.Exceptions;

namespace WA.Pizza.Infrastructure.Services
{
    public class CatalogDataService
    {
        private PizzaContext _dbContext;

        public CatalogDataService(PizzaContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> Create(CatalogItem model)
        {
            var catalogItem = await _dbContext.CatalogItems.AddAsync(model);

            if (catalogItem == null)
                throw new WAPizzaFailedToCreateException();

            
            await _dbContext.SaveChangesAsync();

            return catalogItem.Entity.Id;
        }

        public async Task Delete(int id)
        {
            var item = await _dbContext.CatalogItems.FirstOrDefaultAsync(i => i.Id == id);
            
            if (item == null)
                throw new WAPizzaFailedToFindException();
            
            _dbContext.CatalogItems.Remove(item);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<CatalogItem> Get(int id)
        {
            var item = await _dbContext.CatalogItems.FirstOrDefaultAsync(m => m.Id == id);
            
            if (item == null)
                throw new WAPizzaFailedToFindException();

            return item;
        }
        
        public async Task<List<CatalogItem>> GetAll() => await _dbContext.CatalogItems.ToListAsync();

        public async Task<int> Update(CatalogItem model)
        {
            var item = await _dbContext.CatalogItems.FirstOrDefaultAsync(m => m.Id == model.Id);

            if (item == null)
                throw new WAPizzaFailedToFindException();

            item.Name = model.Name;
            item.Description = model.Description;
            item.Price = model.Price;
            item.Quantity = model.Quantity;

            //catalogItem = _dbContext.CatalogItems.Update(catalogItem).Entity;

            await _dbContext.SaveChangesAsync();

            return item.Id;
        }
    }
}
