namespace Festispec.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update13_48 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Questions", "Options", c => c.String());
            AddColumn("dbo.Questions", "Columns", c => c.String());
            AddColumn("dbo.Questionnaires", "InspectionId", c => c.Int(nullable: false));
            AddColumn("dbo.Questionnaires", "Instructions", c => c.String());
            DropColumn("dbo.Questionnaires", "Name");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Questionnaires", "Name", c => c.String(nullable: false));
            DropColumn("dbo.Questionnaires", "Instructions");
            DropColumn("dbo.Questionnaires", "InspectionId");
            DropColumn("dbo.Questions", "Columns");
            DropColumn("dbo.Questions", "Options");
        }
    }
}
