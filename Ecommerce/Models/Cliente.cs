namespace Ecommerce.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Cliente")]
    public partial class Cliente
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Cliente()
        {
            Carrello = new HashSet<Carrello>();
            IndirizzoSpedizione = new HashSet<IndirizzoSpedizione>();
            MetodoPagamento = new HashSet<MetodoPagamento>();
            Ordine = new HashSet<Ordine>();
        }

        [Key]
        [Display(Name = "Id cliente")]
        public int IdCliente { get; set; }

        [Required]
        [StringLength(30)]
        public string Nome { get; set; }

        [Required]
        [StringLength(30)]
        public string Cognome { get; set; }

        [Column(TypeName = "date")]
        [Display(Name = "Data di nascita")]
        public DateTime DataNascita { get; set; }

        [Required]
        [StringLength(30)]
        public string Telefono { get; set; }

        [Required]
        [StringLength(30)]
        public string Email { get; set; }

        [Required]
        [StringLength(30)]
        public string Citta { get; set; }

        [Required]
        [StringLength(30)]
        public string Provincia { get; set; }

        public int CAP { get; set; }

        [Required]
        [StringLength(30)]
        [Display(Name = "Indirizzo residenza")]
        public string ViaResidenza { get; set; }

        public virtual Login Login { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Carrello> Carrello { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<IndirizzoSpedizione> IndirizzoSpedizione { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MetodoPagamento> MetodoPagamento { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Ordine> Ordine { get; set; }
    }
}
