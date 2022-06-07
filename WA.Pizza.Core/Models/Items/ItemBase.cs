using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WA.Pizza.Core.Models.Items
{
    public abstract class ItemBase : ModelBase
    {
        public string Name { get; set; }
        public float Price { get; set; }
    }
}
