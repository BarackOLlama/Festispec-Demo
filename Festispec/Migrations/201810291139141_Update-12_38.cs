namespace Festispec.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update12_38 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Questions", name: "QuizId", newName: "QuestionnaireId");
            RenameColumn(table: "dbo.Questions", name: "CategoryId", newName: "QuestionTypeId");
            RenameIndex(table: "dbo.Questions", name: "IX_CategoryId", newName: "IX_QuestionTypeId");
            RenameIndex(table: "dbo.Questions", name: "IX_QuizId", newName: "IX_QuestionnaireId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Questions", name: "IX_QuestionnaireId", newName: "IX_QuizId");
            RenameIndex(table: "dbo.Questions", name: "IX_QuestionTypeId", newName: "IX_CategoryId");
            RenameColumn(table: "dbo.Questions", name: "QuestionTypeId", newName: "CategoryId");
            RenameColumn(table: "dbo.Questions", name: "QuestionnaireId", newName: "QuizId");
        }
    }
}
