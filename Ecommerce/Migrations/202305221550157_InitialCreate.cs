namespace Ecommerce.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Carrello",
                c => new
                    {
                        IdCarrello = c.Int(nullable: false, identity: true),
                        IdCliente = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IdCarrello)
                .ForeignKey("dbo.Cliente", t => t.IdCliente)
                .Index(t => t.IdCliente);
            
            CreateTable(
                "dbo.Cliente",
                c => new
                    {
                        IdCliente = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false, maxLength: 30),
                        Cognome = c.String(nullable: false, maxLength: 30),
                        DataNascita = c.DateTime(nullable: false, storeType: "date"),
                        Telefono = c.String(nullable: false, maxLength: 30),
                        Email = c.String(nullable: false, maxLength: 30),
                        Citta = c.String(nullable: false, maxLength: 30),
                        Provincia = c.String(nullable: false, maxLength: 30),
                        CAP = c.Int(nullable: false),
                        ViaResidenza = c.String(nullable: false, maxLength: 30),
                    })
                .PrimaryKey(t => t.IdCliente);
            
            CreateTable(
                "dbo.IndirizzoSpedizione",
                c => new
                    {
                        IdIndirizzoSpedizione = c.Int(nullable: false, identity: true),
                        Indirizzo = c.String(nullable: false, maxLength: 30),
                        Citta = c.String(nullable: false, maxLength: 30),
                        Provincia = c.String(nullable: false, maxLength: 30),
                        Cap = c.Int(nullable: false),
                        IdCliente = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IdIndirizzoSpedizione)
                .ForeignKey("dbo.Cliente", t => t.IdCliente)
                .Index(t => t.IdCliente);
            
            CreateTable(
                "dbo.Ordine",
                c => new
                    {
                        IdOrdine = c.Int(nullable: false, identity: true),
                        IdCliente = c.Int(nullable: false),
                        IdMetodoPagamento = c.Int(nullable: false),
                        IdIndirizzoSpedizione = c.Int(nullable: false),
                        DataOrdine = c.DateTime(nullable: false, storeType: "date"),
                        Tracking = c.Int(nullable: false),
                        TotaleOrdine = c.Decimal(nullable: false, storeType: "money"),
                        IdCarrello = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IdOrdine)
                .ForeignKey("dbo.MetodoPagamento", t => t.IdMetodoPagamento)
                .ForeignKey("dbo.IndirizzoSpedizione", t => t.IdIndirizzoSpedizione)
                .ForeignKey("dbo.Cliente", t => t.IdCliente)
                .ForeignKey("dbo.Carrello", t => t.IdCarrello)
                .Index(t => t.IdCliente)
                .Index(t => t.IdMetodoPagamento)
                .Index(t => t.IdIndirizzoSpedizione)
                .Index(t => t.IdCarrello);
            
            CreateTable(
                "dbo.MetodoPagamento",
                c => new
                    {
                        IdMetodoPagamento = c.Int(nullable: false, identity: true),
                        IdTipologiaMetodo = c.Int(nullable: false),
                        CodiceMetodo = c.String(maxLength: 30),
                        Scadenza = c.DateTime(storeType: "date"),
                        Intestatario = c.String(nullable: false, maxLength: 30),
                        CVV = c.Int(),
                        IdCliente = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IdMetodoPagamento)
                .ForeignKey("dbo.TipologiaMetodoPagamento", t => t.IdTipologiaMetodo)
                .ForeignKey("dbo.Cliente", t => t.IdCliente)
                .Index(t => t.IdTipologiaMetodo)
                .Index(t => t.IdCliente);
            
            CreateTable(
                "dbo.TipologiaMetodoPagamento",
                c => new
                    {
                        IdTipologiaMetodo = c.Int(nullable: false, identity: true),
                        NomeTipologiaMetodo = c.String(nullable: false, maxLength: 30),
                    })
                .PrimaryKey(t => t.IdTipologiaMetodo);
            
            CreateTable(
                "dbo.RecordOrdine",
                c => new
                    {
                        IdRecordOrdine = c.Int(nullable: false, identity: true),
                        IdProdotto = c.Int(nullable: false),
                        IdCarrello = c.Int(nullable: false),
                        QtProdotto = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IdRecordOrdine)
                .ForeignKey("dbo.Prodotto", t => t.IdProdotto)
                .ForeignKey("dbo.Carrello", t => t.IdCarrello)
                .Index(t => t.IdProdotto)
                .Index(t => t.IdCarrello);
            
            CreateTable(
                "dbo.Prodotto",
                c => new
                    {
                        IdProdotto = c.Int(nullable: false, identity: true),
                        NomeProdotto = c.String(nullable: false, maxLength: 30),
                        Promozione = c.Boolean(nullable: false),
                        Prezzo = c.Decimal(nullable: false, storeType: "money"),
                        PrezzoPrecedente = c.Decimal(storeType: "money"),
                        Descrizione = c.String(),
                        IdCategoria = c.Int(nullable: false),
                        FotoUrl = c.String(nullable: false, unicode: false, storeType: "text"),
                        QtStock = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IdProdotto)
                .ForeignKey("dbo.CategoriaProdotto", t => t.IdCategoria)
                .Index(t => t.IdCategoria);
            
            CreateTable(
                "dbo.CategoriaProdotto",
                c => new
                    {
                        IdCategoria = c.Int(nullable: false, identity: true),
                        NomeCategoria = c.String(nullable: false, maxLength: 30),
                    })
                .PrimaryKey(t => t.IdCategoria);
            
            CreateTable(
                "dbo.Login",
                c => new
                    {
                        IdUser = c.Int(nullable: false, identity: true),
                        Username = c.String(nullable: false, maxLength: 30),
                        Password = c.String(nullable: false, maxLength: 50),
                        Ruolo = c.String(nullable: false, maxLength: 10, fixedLength: true),
                    })
                .PrimaryKey(t => t.IdUser);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RecordOrdine", "IdCarrello", "dbo.Carrello");
            DropForeignKey("dbo.RecordOrdine", "IdProdotto", "dbo.Prodotto");
            DropForeignKey("dbo.Prodotto", "IdCategoria", "dbo.CategoriaProdotto");
            DropForeignKey("dbo.Ordine", "IdCarrello", "dbo.Carrello");
            DropForeignKey("dbo.Ordine", "IdCliente", "dbo.Cliente");
            DropForeignKey("dbo.MetodoPagamento", "IdCliente", "dbo.Cliente");
            DropForeignKey("dbo.IndirizzoSpedizione", "IdCliente", "dbo.Cliente");
            DropForeignKey("dbo.Ordine", "IdIndirizzoSpedizione", "dbo.IndirizzoSpedizione");
            DropForeignKey("dbo.MetodoPagamento", "IdTipologiaMetodo", "dbo.TipologiaMetodoPagamento");
            DropForeignKey("dbo.Ordine", "IdMetodoPagamento", "dbo.MetodoPagamento");
            DropForeignKey("dbo.Carrello", "IdCliente", "dbo.Cliente");
            DropIndex("dbo.Prodotto", new[] { "IdCategoria" });
            DropIndex("dbo.RecordOrdine", new[] { "IdCarrello" });
            DropIndex("dbo.RecordOrdine", new[] { "IdProdotto" });
            DropIndex("dbo.MetodoPagamento", new[] { "IdCliente" });
            DropIndex("dbo.MetodoPagamento", new[] { "IdTipologiaMetodo" });
            DropIndex("dbo.Ordine", new[] { "IdCarrello" });
            DropIndex("dbo.Ordine", new[] { "IdIndirizzoSpedizione" });
            DropIndex("dbo.Ordine", new[] { "IdMetodoPagamento" });
            DropIndex("dbo.Ordine", new[] { "IdCliente" });
            DropIndex("dbo.IndirizzoSpedizione", new[] { "IdCliente" });
            DropIndex("dbo.Carrello", new[] { "IdCliente" });
            DropTable("dbo.Login");
            DropTable("dbo.CategoriaProdotto");
            DropTable("dbo.Prodotto");
            DropTable("dbo.RecordOrdine");
            DropTable("dbo.TipologiaMetodoPagamento");
            DropTable("dbo.MetodoPagamento");
            DropTable("dbo.Ordine");
            DropTable("dbo.IndirizzoSpedizione");
            DropTable("dbo.Cliente");
            DropTable("dbo.Carrello");
        }
    }
}
