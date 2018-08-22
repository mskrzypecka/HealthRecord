namespace HealthRecord.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ImageLinkAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Creations", "ImageLink", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Creations", "ImageLink");
        }
    }
}
