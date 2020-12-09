using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProductShop.API.Domain
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Image { get; set; }

        [Required]
        public decimal Amount { get; set; }
        
        public string Detail { get; set; }
        public int Stock { get; set; }

    }
}
