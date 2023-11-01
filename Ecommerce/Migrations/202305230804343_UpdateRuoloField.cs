namespace Ecommerce.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateRuoloField : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Login", "Ruolo", c => c.String(nullable: false, maxLength: 50, fixedLength: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Login", "Ruolo", c => c.String(nullable: false, maxLength: 10, fixedLength: false));
        }
    }
}
