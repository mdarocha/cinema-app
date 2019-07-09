namespace CinemaApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PlaceModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Places", "x", c => c.Int(nullable: false));
            AddColumn("dbo.Places", "y", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Places", "y");
            DropColumn("dbo.Places", "x");
        }
    }
}
