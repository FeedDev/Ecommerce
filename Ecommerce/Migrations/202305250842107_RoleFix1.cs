namespace Ecommerce.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RoleFix1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Login", "Ruolo", c => c.String(nullable: false, maxLength: 6, fixedLength: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Login", "Ruolo", c => c.String(nullable: false, maxLength: 8, fixedLength: true));
        }
    }
}
