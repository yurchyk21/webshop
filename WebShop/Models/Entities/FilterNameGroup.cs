using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebShop.Models.Entities
{
    [Table("tblFilterNameGroups")]
    public class FilterNameGroup
    {
        [Key, Column(Order = 0)]
        public int Id { get; set; }
        [ForeignKey("FilterNameOf"), Column(Order = 1)]

        public int FilterNameId { get; set; }
        public virtual FilterName FilterNameOf { get; set; }

        [ForeignKey("FilterValueOf"), Column(Order = 2)]
        public int FilterValueId { get; set; }
        public virtual FilterValue FilterValueOf { get; set; }

    }
}