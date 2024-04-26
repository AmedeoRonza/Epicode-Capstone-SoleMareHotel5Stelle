namespace Albergo.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Galleria")]
    public partial class Galleria
    {
        [Key]
        public int IdGalleria { get; set; }

        [Required]
        [StringLength(50)]
        public string TipoStanza { get; set; }

        [Required]
        public string Img1 { get; set; }

        [Required]
        public string Img2 { get; set; }

        [Required]
        public string Img3 { get; set; }

        [Required]
        public string Descrizione { get; set; }
    }
}
