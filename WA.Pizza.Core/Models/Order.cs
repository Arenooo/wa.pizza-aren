using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WA.Pizza.Core.Models.Items;

namespace WA.Pizza.Core.Models
{
    public class Order : ModelBase
    {
        public ICollection<OrderItem> Items { get; set; }
    }
}
