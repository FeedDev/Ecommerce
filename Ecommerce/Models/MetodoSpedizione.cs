namespace Ecommerce.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("MetodoSpedizione")]
    public partial class MetodoSpedizione
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]

        [Key]
        [Display(Name = "Id metodo spedizione")]
        public int IdMetodoSpedizione { get; set; }

        [Required]
        [StringLength(30)]
        [Display(Name = "Tipo spedizione")]
        public string TipoSpedizione { get; set; }

        [Required]
        public decimal Costo { get; set; }

        [Required]
        [Display(Name = "Tempistiche spedizione")]
        public int TempisticheSpedizione { get; set; }

        [Required]
        public string Corriere { get; set; }
    }
}
