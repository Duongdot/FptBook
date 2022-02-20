namespace FptBookNew1.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class feedback
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(50)]
        public string username { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(400)]
        public string contentFeedback { get; set; }

        public virtual account account { get; set; }
    }
}
