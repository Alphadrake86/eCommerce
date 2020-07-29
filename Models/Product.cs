using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerce.Models
{
    /// <summary>
    /// A sellable product
    /// </summary>
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        /// <summary>
        /// The title of the product
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// The price of the currency, in US dollars
        /// </summary>
        public double Price { get; set; }

        /// <summary>
        /// the user generated category this product falls under (Eg. Appliances, Electronics, Kitchen, etc.)
        /// </summary>
        public string Category { get; set; }
    }
}
