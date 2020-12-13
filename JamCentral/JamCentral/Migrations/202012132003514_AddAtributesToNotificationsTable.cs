namespace JamCentral.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAtributesToNotificationsTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Notifications", "GigNewLocation", c => c.String());
            AddColumn("dbo.Notifications", "GigNewDateTime", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Notifications", "GigNewDateTime");
            DropColumn("dbo.Notifications", "GigNewLocation");
        }
    }
}
