namespace JamCentral.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNavigationPropertyOnUserModel : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Followings", "ArtistId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Followings", "UserId", "dbo.AspNetUsers");
            AddForeignKey("dbo.Followings", "ArtistId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Followings", "UserId", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Followings", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Followings", "ArtistId", "dbo.AspNetUsers");
            AddForeignKey("dbo.Followings", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Followings", "ArtistId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
    }
}
