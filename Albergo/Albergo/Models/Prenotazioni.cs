namespace Albergo.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Prenotazioni")]
    public partial class Prenotazioni
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Prenotazioni()
        {
            PrenotazioniServizi = new HashSet<PrenotazioniServizi>();
        }

        [Key]
        public int IdPrenotazione { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        public string Cognome { get; set; }

        [Required]
        [StringLength(35)]
        [Display(Name = "Città")]
        public string Citta { get; set; }

        [Required(ErrorMessage = "Il campo Email è obbligatorio")]
        [EmailAddress(ErrorMessage = "Indirizzo Email non valido")]
        public string Email { get; set; }

        [Required]
        [Column(TypeName = "date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DataCheckIn { get; set; }

        [Required]
        [Column(TypeName = "date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DataCheckOut { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Tipologia Stanza")]
        public string TipoStanza { get; set; }
        [Required]
        public int Numero { get; set; }

        public decimal Prezzo { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PrenotazioniServizi> PrenotazioniServizi { get; set; }
    }
}
