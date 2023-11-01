namespace Ecommerce.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveTipologiaMetodoPagamento : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.MetodoPagamento", "IdTipologiaMetodo", "dbo.TipologiaMetodoPagamento");
            DropIndex("dbo.MetodoPagamento", new[] { "IdTipologiaMetodo" });
            AddColumn("dbo.MetodoPagamento", "TipologiaMetodoPagamento", c => c.String());
            DropTable("dbo.TipologiaMetodoPagamento");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.TipologiaMetodoPagamento",
                c => new
                    {
                        IdTipologiaMetodo = c.Int(nullable: false, identity: true),
                        NomeTipologiaMetodo = c.String(nullable: false, maxLength: 30),
                    })
                .PrimaryKey(t => t.IdTipologiaMetodo);
            
            DropColumn("dbo.MetodoPagamento", "TipologiaMetodoPagamento");
            CreateIndex("dbo.MetodoPagamento", "IdTipologiaMetodo");
            AddForeignKey("dbo.MetodoPagamento", "IdTipologiaMetodo", "dbo.TipologiaMetodoPagamento", "IdTipologiaMetodo");
        }
    }
}
