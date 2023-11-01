namespace Ecommerce.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCodiceSconto : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CodiceSconto",
                c => new
                    {
                        IdCodiceSconto = c.Int(nullable: false, identity: true),
                        Codice = c.String(),
                        IdCliente = c.Int(nullable: false),
                        Scadenza = c.DateTime(nullable: false),
                        Usato = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.IdCodiceSconto)
                .ForeignKey("dbo.Cliente", t => t.IdCliente, cascadeDelete: true)
                .Index(t => t.IdCliente);
            
            AddColumn("dbo.Carrello", "IdCodiceSconto", c => c.Int());
            CreateIndex("dbo.Carrello", "IdCodiceSconto");
            AddForeignKey("dbo.Carrello", "IdCodiceSconto", "dbo.CodiceSconto", "IdCodiceSconto");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Carrello", "IdCodiceSconto", "dbo.CodiceSconto");
            DropForeignKey("dbo.CodiceSconto", "IdCliente", "dbo.Cliente");
            DropIndex("dbo.CodiceSconto", new[] { "IdCliente" });
            DropIndex("dbo.Carrello", new[] { "IdCodiceSconto" });
            DropColumn("dbo.Carrello", "IdCodiceSconto");
            DropTable("dbo.CodiceSconto");
        }
    }
}
