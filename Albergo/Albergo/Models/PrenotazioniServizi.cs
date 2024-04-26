namespace Albergo.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PrenotazioniServizi")]
    public partial class PrenotazioniServizi
    {
        [Key]
        public int IdChiave { get; set; }

        public int IdPrenotazione { get; set; }
        [Required]
        [Display(Name = "Servizio")]
        public int IdServizio { get; set; }

        [Required]
        [Display(Name = "Data inizio servizio")]
        [Column(TypeName = "date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DataInizioServizio { get; set; }

        [Required]
        [Display(Name = "Data fine servizio")]
        [Column(TypeName = "date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DataFineServizio { get; set; }

        public decimal Prezzo { get; set; }

        public virtual Prenotazioni Prenotazioni { get; set; }

        public virtual Servizi Servizi { get; set; }
    }
}
