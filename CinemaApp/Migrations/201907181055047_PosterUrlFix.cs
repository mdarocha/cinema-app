namespace CinemaApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PosterUrlFix : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Movies", "PosterUrl", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Movies", "PosterUrl", c => c.String(nullable: false));
        }
    }
}
