using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mapster;
using WA.Pizza.Core.Models;
using WA.Pizza.Core.Models.Items;
using WA.Pizza.Core.Exceptions;
using WA.Pizza.Infrastructure.DTO.Catalog;

namespace WA.Pizza.Infrastructure.Data.Services
{
    public class CatalogDataService
    {
        private PizzaContext _dbContext;

        public CatalogDataService(PizzaContext dbContext)
        {
            MappingConfigurer.Configure();
            _dbContext = dbContext;
        }

        public async Task<int> Create(CreateCatalogItemRequest request)
        {
            var catalogItem = request.Adapt<CatalogItem>();

            if (catalogItem == null)
                throw new WAPizzaFailedToCreateException();

            _dbContext.CatalogItems.Add(catalogItem);
            
            await _dbContext.SaveChangesAsync();

            return catalogItem.Id;
        }

        public async Task Delete(int id)
        {
            var item = await _dbContext.CatalogItems.FirstOrDefaultAsync(i => i.Id == id);
            
            if (item == null)
                throw new WAPizzaFailedToFindException();
            
            _dbContext.CatalogItems.Remove(item);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<CatalogItemDTO> Get(int id)
        {
            var item = await _dbContext.CatalogItems.FirstOrDefaultAsync(m => m.Id == id);
            
            if (item == null)
                throw new WAPizzaFailedToFindException();

            return item.Adapt<CatalogItemDTO>();
        }
        
        public async Task<List<CatalogItemDTO>> GetAll() => await _dbContext.CatalogItems.ProjectToType<CatalogItemDTO>().ToListAsync();

        public async Task<int> Update(UpdateCatalogItemRequest request)
        {
            var item = await _dbContext.CatalogItems.FirstOrDefaultAsync(m => m.Id == request.Id);

            if (item == null)
                throw new WAPizzaFailedToFindException();

            request.Adapt(item);

            await _dbContext.SaveChangesAsync();

            return item.Id;
        }
    }
}
