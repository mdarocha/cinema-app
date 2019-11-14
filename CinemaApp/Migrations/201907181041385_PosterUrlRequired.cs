namespace CinemaApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PosterUrlRequired : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Movies", "PosterUrl", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Movies", "PosterUrl", c => c.String());
        }
    }
}
