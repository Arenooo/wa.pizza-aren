using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WA.Pizza.Core.Models.Items;

namespace WA.Pizza.Core.Models
{
    public class Basket : ModelBase
    {
        public int CustomerId { get; set; }
        public ICollection<BasketItem> Items { get; set; }
    }
}
