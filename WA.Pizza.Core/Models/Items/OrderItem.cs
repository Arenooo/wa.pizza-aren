using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WA.Pizza.Core.Models.Items
{
    public class OrderItem : ItemBase 
    {
        //public int OrderId { get; set; }
        //public int CatalogItemId { get; set; }
        public int Quantity { get; set; }
        public CatalogItem CatalogItem { get; set; }
        public Order Order { get; set; }
    }
}
