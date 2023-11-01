namespace Ecommerce.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Carrello")]
    public partial class Carrello
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Carrello()
        {
            Ordine = new HashSet<Ordine>();
            RecordOrdine = new HashSet<RecordOrdine>();
        }

        [Key]
        [Display(Name ="Id Carrello")]
        public int IdCarrello { get; set; }

        [Display(Name = "Cliente")]
        public int IdCliente { get; set; }

        public virtual Cliente Cliente { get; set; }

        public int? IdCodiceSconto { get; set; }

        public virtual CodiceSconto CodiceSconto { get; set; }

        public bool Scaduto { get; set; } = false;
        [Display(Name = "Data creazione")]
        public DateTime DataCreazione { get; set; } = DateTime.Now;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Ordine> Ordine { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RecordOrdine> RecordOrdine { get; set; }
    }
}
