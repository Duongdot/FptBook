namespace FptBookNew1.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public  class order
    {
        public order()
        {
            orderDetails = new HashSet<orderDetail>();
        }
        public int orderID { get; set; }

        [Required]
        [StringLength(50)]
        public string username { get; set; }

        [Required]
        [StringLength(13)]
        public string phone { get; set; }

        [Required]
        [StringLength(100)]
        public string address { get; set; }

        public DateTime orderDate { get; set; }

        public int totalPrice { get; set; }

        public virtual account account { get; set; }

     
        public  ICollection<orderDetail> orderDetails { get; set; }
    }
}
