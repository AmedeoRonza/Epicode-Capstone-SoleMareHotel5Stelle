namespace Albergo.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Utenti")]
    public partial class Utenti
    {
        [Key]
        public int IdUtente { get; set; }

        [Required(ErrorMessage = "Il campo Nome è obbligatorio")]
        [StringLength(50)]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Il campo Cognome è obbligatorio")]
        [StringLength(50)]
        public string Cognome { get; set; }

        [Required(ErrorMessage = "Il campo Email è obbligatorio")]
        [EmailAddress(ErrorMessage = "Indirizzo Email non valido")]
        [Index(IsUnique = true)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Il campo Password è obbligatorio")]
        [StringLength(50)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Il campo Conferma Password è obbligatorio")]
        [StringLength(50)]
        [Compare("Password", ErrorMessage = "I campi Password e Conferma Password non corrispondono")]
        [Display(Name = "Conferma Password")]
        [DataType(DataType.Password)]
        public string ConfermaPassword { get; set; }

        [Required]
        [StringLength(50)]
        public string Ruolo { get; set; }
    }
}
