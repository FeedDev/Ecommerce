namespace Ecommerce.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("RecordOrdine")]
    public partial class RecordOrdine
    {
        [Key]
        [Display(Name = "Id record")]
        public int IdRecordOrdine { get; set; }

        [Display(Name = "Id prodotto")]
        public int IdProdotto { get; set; }

        public int IdCarrello { get; set; }

        [Display(Name = "Quantità")]
        public int QtProdotto { get; set; }

        public virtual Carrello Carrello { get; set; }

        public virtual Prodotto Prodotto { get; set; }
    }
}
