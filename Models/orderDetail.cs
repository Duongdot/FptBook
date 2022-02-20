namespace FptBookNew1.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class orderDetail
    {
        [Key]
        [Column(Order = 0)]
        public int orderID { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(10)]
        public string bookID { get; set; }

        public int quantity { get; set; }

        public int amountPrice { get; set; }

        public virtual book book { get; set; }

        public virtual order order { get; set; }
    }
}
