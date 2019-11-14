namespace CinemaApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ShowingMetada : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Showings", "Is2D", c => c.Boolean(nullable: false));
            AddColumn("dbo.Showings", "IsSubtitles", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Showings", "IsSubtitles");
            DropColumn("dbo.Showings", "Is2D");
        }
    }
}
