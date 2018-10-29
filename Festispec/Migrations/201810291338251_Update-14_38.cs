namespace Festispec.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update14_38 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Questionnaires", "Name", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Questionnaires", "Name");
        }
    }
}
