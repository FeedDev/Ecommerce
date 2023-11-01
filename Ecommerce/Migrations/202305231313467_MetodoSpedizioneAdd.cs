namespace Ecommerce.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MetodoSpedizioneAdd : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MetodoSpedizione",
                c => new
                    {
                        IdMetodoSpedizione = c.Int(nullable: false, identity: true),
                        TipoSpedizione = c.String(nullable: false, maxLength: 30),
                        Costo = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TempisticheSpedizione = c.Int(nullable: false),
                        Corriere = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.IdMetodoSpedizione);
            
            AddColumn("dbo.Ordine", "IdMetodoSpedizione", c => c.Int(nullable: false));
            CreateIndex("dbo.Ordine", "IdMetodoSpedizione");
            AddForeignKey("dbo.Ordine", "IdMetodoSpedizione", "dbo.MetodoSpedizione", "IdMetodoSpedizione", cascadeDelete: true);
            DropColumn("dbo.Ordine", "Tracking");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Ordine", "Tracking", c => c.Int(nullable: false));
            DropForeignKey("dbo.Ordine", "IdMetodoSpedizione", "dbo.MetodoSpedizione");
            DropIndex("dbo.Ordine", new[] { "IdMetodoSpedizione" });
            DropColumn("dbo.Ordine", "IdMetodoSpedizione");
            DropTable("dbo.MetodoSpedizione");
        }
    }
}
