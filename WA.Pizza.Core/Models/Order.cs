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
        public int CustomerId { get; set; }
        public ICollection<OrderItem> Items { get; set; }
        public float Price { get; set; }
        public DateTime Date { get; set; }
        public OrderStatus Status { get; set; }

        public enum OrderStatus
        {
            InProgress,
            Delivered,
            Cancelled
        }
    }
}
