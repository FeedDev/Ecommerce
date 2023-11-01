namespace Ecommerce.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddLoginCliente : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Cliente", "IdLogin", c => c.Int(nullable: false));
            AddColumn("dbo.Cliente", "Login_IdUser", c => c.Int());
            CreateIndex("dbo.Cliente", "Login_IdUser");
            AddForeignKey("dbo.Cliente", "Login_IdUser", "dbo.Login", "IdUser");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Cliente", "Login_IdUser", "dbo.Login");
            DropIndex("dbo.Cliente", new[] { "Login_IdUser" });
            DropColumn("dbo.Cliente", "Login_IdUser");
            DropColumn("dbo.Cliente", "IdLogin");
        }
    }
}
