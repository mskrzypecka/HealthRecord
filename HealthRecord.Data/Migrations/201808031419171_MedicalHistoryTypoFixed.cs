namespace HealthRecord.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MedicalHistoryTypoFixed : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MedicalHistoryRecords", "Description", c => c.String());
            DropColumn("dbo.MedicalHistoryRecords", "Descryption");
        }
        
        public override void Down()
        {
            AddColumn("dbo.MedicalHistoryRecords", "Descryption", c => c.String());
            DropColumn("dbo.MedicalHistoryRecords", "Description");
        }
    }
}
