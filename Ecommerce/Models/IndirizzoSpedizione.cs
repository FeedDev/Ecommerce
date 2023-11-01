namespace Ecommerce.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("IndirizzoSpedizione")]
    public partial class IndirizzoSpedizione
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public IndirizzoSpedizione()
        {
            Ordine = new HashSet<Ordine>();
        }

        [Key]
        [Display(Name = "Id indirizzo spedizione")]
        public int IdIndirizzoSpedizione { get; set; }

        [Required]
        [StringLength(30)]
        public string Indirizzo { get; set; }

        [Required]
        [StringLength(30)]
        public string Citta { get; set; }

        [Required]
        [StringLength(30)]
        public string Provincia { get; set; }

        public int Cap { get; set; }

        [Display(Name = "Cliente")]
        public int IdCliente { get; set; }

        public virtual Cliente Cliente { get; set; }


        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Ordine> Ordine { get; set; }
    }
}
