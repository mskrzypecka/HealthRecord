namespace HealthRecord.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreationMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Creations",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        DateOfBirth = c.DateTime(nullable: false),
                        Sex = c.Int(nullable: false),
                        MemberType = c.Int(nullable: false),
                        Weight = c.Double(nullable: false),
                        Name = c.String(),
                        PassportNumber = c.String(),
                        IsSterile = c.Boolean(),
                        PESEL = c.Long(),
                        FirstName = c.String(),
                        LastName = c.String(),
                        PhoneNumber = c.String(),
                        Address = c.String(),
                        Region = c.Int(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                        Owner_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Creations", t => t.Owner_Id)
                .Index(t => t.Owner_Id);
            
            CreateTable(
                "dbo.MedicalHistoryRecords",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                        Descryption = c.String(),
                        DateOfVaccination = c.DateTime(nullable: false),
                        RecordType = c.Int(nullable: false),
                        Creation_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Creations", t => t.Creation_Id)
                .Index(t => t.Creation_Id);
            
            CreateTable(
                "dbo.Vaccines",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                        Serie = c.Long(nullable: false),
                        DateOfVaccination = c.DateTime(nullable: false),
                        Creation_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Creations", t => t.Creation_Id)
                .Index(t => t.Creation_Id);
            
            CreateTable(
                "dbo.Chips",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                        Number = c.Long(nullable: false),
                        ChipDate = c.DateTime(nullable: false),
                        Animal_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Creations", t => t.Animal_Id)
                .Index(t => t.Animal_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Creations", "Owner_Id", "dbo.Creations");
            DropForeignKey("dbo.Chips", "Animal_Id", "dbo.Creations");
            DropForeignKey("dbo.Vaccines", "Creation_Id", "dbo.Creations");
            DropForeignKey("dbo.MedicalHistoryRecords", "Creation_Id", "dbo.Creations");
            DropIndex("dbo.Chips", new[] { "Animal_Id" });
            DropIndex("dbo.Vaccines", new[] { "Creation_Id" });
            DropIndex("dbo.MedicalHistoryRecords", new[] { "Creation_Id" });
            DropIndex("dbo.Creations", new[] { "Owner_Id" });
            DropTable("dbo.Chips");
            DropTable("dbo.Vaccines");
            DropTable("dbo.MedicalHistoryRecords");
            DropTable("dbo.Creations");
        }
    }
}
