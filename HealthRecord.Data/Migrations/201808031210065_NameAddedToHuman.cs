namespace HealthRecord.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NameAddedToHuman : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Creations", "Name");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Creations", "Name", c => c.String());
        }
    }
}
