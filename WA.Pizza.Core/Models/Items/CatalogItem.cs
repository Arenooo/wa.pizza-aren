using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WA.Pizza.Core.Models.Items
{
    public class CatalogItem
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public int Quantity { get; set; }
    }
}
