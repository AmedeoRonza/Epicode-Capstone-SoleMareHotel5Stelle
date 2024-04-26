namespace Albergo.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Servizi")]
    public partial class Servizi
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Servizi()
        {
            PrenotazioniServizi = new HashSet<PrenotazioniServizi>();
        }

        [Key]
        public int IdServizio { get; set; }

        [Required]
        [StringLength(100)]
        public string Descrizione { get; set; }

        public decimal Prezzo { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PrenotazioniServizi> PrenotazioniServizi { get; set; }
    }
}
