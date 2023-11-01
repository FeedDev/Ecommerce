namespace Ecommerce.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web;

    [Table("Prodotto")]
    public partial class Prodotto
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Prodotto()
        {
            RecordOrdine = new HashSet<RecordOrdine>();
        }
        
        [NotMapped]
        public HttpPostedFileBase Photo { get; set; }

        [Key]
        public int IdProdotto { get; set; }

        [Required]
        [StringLength(30)]
        [Display(Name = "Nome prodotto")]
        public string NomeProdotto { get; set; }

        public bool Promozione { get; set; }

        [Column(TypeName = "money")]
        public decimal Prezzo { get; set; }

        [Column(TypeName = "money")]
        [Display(Name = "Prezzo precedente")]
        public decimal? PrezzoPrecedente { get; set; }

        public string Descrizione { get; set; }

        [Display(Name = "Categoria")]
        public int IdCategoria { get; set; }

        [Column(TypeName = "text")]
        [Required]
        [Display(Name = "Foto")]
        public string FotoUrl { get; set; }

        [Display(Name = "Quantità stock")]
        public int QtStock { get; set; }

        public virtual CategoriaProdotto CategoriaProdotto { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RecordOrdine> RecordOrdine { get; set; }
    }

    public class ProdottoJson
    {
        public int IdProdotto { get; set; }

        [Required]
        [StringLength(30)]
        public string NomeProdotto { get; set; }

        [Column(TypeName = "money")]
        public decimal Prezzo { get; set; }

        [Column(TypeName = "money")]
        public decimal? PrezzoPrecedente { get; set; }

        public string Descrizione { get; set; }

        public int IdCategoria { get; set; }

        [Column(TypeName = "text")]
        [Required]
        public string FotoUrl { get; set; }

        public int QtStock { get; set; }
    }
}
