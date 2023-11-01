namespace Ecommerce.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CarrelloFix : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Carrello", "Scaduto", c => c.Boolean(nullable: false));
            AddColumn("dbo.Carrello", "DataCreazione", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Carrello", "DataCreazione");
            DropColumn("dbo.Carrello", "Scaduto");
        }
    }
}
