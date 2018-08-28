using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebShop.Models.Entities
{
    [Table("tblProductImage")]
    public class ProductImage
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "File")]
        [Required, StringLength(maximumLength:255)]
        public string FileName { get; set; }
    }
}