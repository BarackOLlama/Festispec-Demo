namespace Festispec.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update14_02 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.QuestionTypes", "Name", c => c.String(nullable: false, maxLength: 20));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.QuestionTypes", "Name", c => c.String(nullable: false, maxLength: 10));
        }
    }
}
