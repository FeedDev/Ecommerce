namespace Ecommerce.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;

    [Table("CategoriaProdotto")]
    public partial class CategoriaProdotto
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CategoriaProdotto()
        {
            Prodotto = new HashSet<Prodotto>();
        }

        [Key]
        public int IdCategoria { get; set; }

        [Required]
        [StringLength(30)]
        [Display(Name = "Nome categoria")]
        public string NomeCategoria { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Prodotto> Prodotto { get; set; }
    }

    public class CategoriaProdottoJson
    {
        [Key]
        public int IdCategoria { get; set; }

        [Required]
        [StringLength(30)]
        public string NomeCategoria { get; set; }

        public static List<CategoriaProdottoJson> listaCategorie()
        {
            List<CategoriaProdottoJson> ListToReturn = new List<CategoriaProdottoJson>();
            ModelDbContext modelDBContext = new ModelDbContext();
            foreach (CategoriaProdotto t in modelDBContext.CategoriaProdotto.ToList())
            {
                CategoriaProdottoJson categoria = new CategoriaProdottoJson();
                categoria.IdCategoria = t.IdCategoria;
                categoria.NomeCategoria = t.NomeCategoria;
                ListToReturn.Add(categoria);
            }
            return ListToReturn;
        }
    }
}
