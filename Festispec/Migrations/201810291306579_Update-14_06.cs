namespace Festispec.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update14_06 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Answers", "Correct");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Answers", "Correct", c => c.Boolean(nullable: false));
        }
    }
}
