namespace Ecommerce.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixScadenza : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.MetodoPagamento", "Scadenza", c => c.DateTime(nullable: false, storeType: "date"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.MetodoPagamento", "Scadenza", c => c.DateTime(storeType: "date"));
        }
    }
}
