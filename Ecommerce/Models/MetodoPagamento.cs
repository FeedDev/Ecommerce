namespace Ecommerce.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("MetodoPagamento")]
    public partial class MetodoPagamento
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MetodoPagamento()
        {
            Ordine = new HashSet<Ordine>();
        }

        [Key]
        [Display(Name = "Id metodo")]
        public int IdMetodoPagamento { get; set; }

        [Display(Name = "Tipologia metodo")]
        public int IdTipologiaMetodo { get; set; }

        [StringLength(30)]
        [Display(Name = "Codice")]
        public string CodiceMetodo { get; set; }

        [Column(TypeName = "date")]
        public DateTime Scadenza { get; set; }

        [Required]
        [StringLength(30)]
        public string Intestatario { get; set; }

        public int? CVV { get; set; }

        [Display(Name = "Cliente")]
        public int IdCliente { get; set; }

        public virtual Cliente Cliente { get; set; }

        [Display(Name = "Tipologia metodo")]
        public string TipologiaMetodoPagamento { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Ordine> Ordine { get; set; }
    }
}
