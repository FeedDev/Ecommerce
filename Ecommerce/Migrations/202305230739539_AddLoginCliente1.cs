namespace Ecommerce.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddLoginCliente1 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Cliente", "IdLogin");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Cliente", "IdLogin", c => c.Int(nullable: false));
        }
    }
}
