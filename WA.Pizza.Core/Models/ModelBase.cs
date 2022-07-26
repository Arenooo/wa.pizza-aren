using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WA.Pizza.Core.Models
{
    public abstract class ModelBase
    {
        [Key]
        public int Id { get; set; }
    }
}
