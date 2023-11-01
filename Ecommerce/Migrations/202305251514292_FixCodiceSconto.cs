namespace Ecommerce.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixCodiceSconto : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CodiceSconto", "Percentuale", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CodiceSconto", "Percentuale");
        }
    }
}
