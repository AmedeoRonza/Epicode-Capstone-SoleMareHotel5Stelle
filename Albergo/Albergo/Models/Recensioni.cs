namespace Albergo.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Recensioni")]
    public partial class Recensioni
    {
        [Key]
        public int IdReview { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        public string Cognome { get; set; }
        [Required]
        public int Voto { get; set; }

        [Required]
        [Display(Name = "Recensione")]
        public string TestoRecensione { get; set; }

        public bool Approvata { get; set; }
    }
}
