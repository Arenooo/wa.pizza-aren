using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WA.Pizza.Core.Models.Items
{
    public class BasketItem : ItemBase
    {
        //public int BasketId { get; set; }
        //public int CatalogItemId { get; set; }
        public int Quantity { get; set; }
        public CatalogItem CatalogItem { get; set; }
        public Basket Basket { get; set; }
    }
}
