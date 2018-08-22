namespace HealthRecord.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserCreationRelationMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Creations", "Account_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Creations", "Account_Id");
            AddForeignKey("dbo.Creations", "Account_Id", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Creations", "Account_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Creations", new[] { "Account_Id" });
            DropColumn("dbo.Creations", "Account_Id");
        }
    }
}
