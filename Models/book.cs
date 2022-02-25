namespace FptBookNew1.Models
{

    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web;

    public partial class book
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public book()
        {
            orderDetails = new HashSet<orderDetail>();
        }

        [StringLength(10)]
        public string bookID { get; set; }

        [Required]
        [StringLength(100)]
        public string bookName { get; set; }

        [Required]
        [StringLength(10)]
        public string categoryID { get; set; }

        [Required]
        [StringLength(10)]
        public string authorID { get; set; }

        public int quantity { get; set; }

        public int price { get; set; }

        //[Required]
        [StringLength(500)]
        //public string image { get; set; }
        [Required]
        //[DataType(DataType.Upload)]
        //[Display(Name = "Choose File")]
        public string image { get; set; }

        [Required]
        [StringLength(200)]
        public string shortDesc { get; set; }

        [Required]
        [StringLength(500)]
        public string detailDesc { get; set; }

        public virtual author author { get; set; }

        public virtual category category { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<orderDetail> orderDetails { get; set; }
    }
}
