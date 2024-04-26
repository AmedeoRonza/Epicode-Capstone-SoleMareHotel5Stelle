namespace Albergo.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Camere")]
    public partial class Camere
    {
        [Key]
        public int IdCamera { get; set; }

        [Required]
        [StringLength(50)]
        public string Piano { get; set; }

        public int NumeroCamera { get; set; }

        [Required]
        [StringLength(15)]
        public string Tipo { get; set; }

        [Required]
        public string Descrizione { get; set; }

        public decimal Prezzo { get; set; }
    }
}
