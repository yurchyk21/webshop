using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebShop.Models.Entities
{
    [Table("tblProductImageBaskets")]
    public class ProductImageBasket
    {
        [Key]
        public int Id { get; set; }
        [StringLength(150)]
        public string Name { get; set; }
    }
}