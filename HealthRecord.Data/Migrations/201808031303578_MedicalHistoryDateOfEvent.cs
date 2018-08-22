namespace HealthRecord.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MedicalHistoryDateOfEvent : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MedicalHistoryRecords", "DateOfEvent", c => c.DateTime(nullable: false));
            DropColumn("dbo.MedicalHistoryRecords", "DateOfVaccination");
        }
        
        public override void Down()
        {
            AddColumn("dbo.MedicalHistoryRecords", "DateOfVaccination", c => c.DateTime(nullable: false));
            DropColumn("dbo.MedicalHistoryRecords", "DateOfEvent");
        }
    }
}
