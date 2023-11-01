namespace Ecommerce.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CodiceSconto")]
    public partial class CodiceSconto
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]

        [Key]
        [Display(Name = "Id Codice Sconto")]
        public int IdCodiceSconto { get; set; }

        public string Codice { get; set; }

        [Display(Name = "Cliente")]
        public int IdCliente { get; set; }

        public int Percentuale { get; set; }

        public DateTime Scadenza { get; set; }

        public virtual Cliente Cliente { get; set; }

        public bool Usato { get; set; } = false;
    }
}