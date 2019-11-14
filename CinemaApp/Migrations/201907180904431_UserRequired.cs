namespace CinemaApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserRequired : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AspNetUsers", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.AspNetUsers", "Surname", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AspNetUsers", "Surname", c => c.String());
            AlterColumn("dbo.AspNetUsers", "Name", c => c.String());
        }
    }
}
