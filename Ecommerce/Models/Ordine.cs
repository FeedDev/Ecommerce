namespace Ecommerce.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Ordine")]
    public partial class Ordine
    {
        [Key]
        [Display(Name = "Id ordine")]
        public int IdOrdine { get; set; }

        [Display(Name = "Cliente")]
        public int IdCliente { get; set; }

        [Display(Name = "Metodo pagamento")]
        public int IdMetodoPagamento { get; set; }

        [Display(Name = "Indirizzo spedizione")]
        public int IdIndirizzoSpedizione { get; set; }

        [Column(TypeName = "date")]
        [Display(Name = "Data ordine")]
        public DateTime DataOrdine { get; set; }

        [Display(Name = "Metodo spedizione")]
        public int IdMetodoSpedizione { get; set; }

        [Column(TypeName = "money")]
        [Display(Name = "Totale ordine")]
        public decimal TotaleOrdine { get; set; }

        public int IdCarrello { get; set; }

        public virtual Carrello Carrello { get; set; }

        public virtual Cliente Cliente { get; set; }

        public virtual IndirizzoSpedizione IndirizzoSpedizione { get; set; }

        public virtual MetodoPagamento MetodoPagamento { get; set; }
        public virtual MetodoSpedizione MetodoSpedizione { get; set; }
    }
}
