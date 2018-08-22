namespace HealthRecord.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NameTypeChangedToString : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Creations", "Name", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Creations", "Name");
        }
    }
}
