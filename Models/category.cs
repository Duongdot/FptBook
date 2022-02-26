namespace FptBookNew1.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;


    public class category
    {
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public category()
        {
            books = new HashSet<book>();
        }
        [StringLength(10)]
        public string categoryID { get; set; }

        [Required]
        [StringLength(50)]
        public string categoryName { get; set; }

        [StringLength(100)]
        public string description { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public  ICollection<book> books { get; set; }
    }
}
