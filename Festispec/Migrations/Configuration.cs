namespace Festispec.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Festispec.FestispecContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(Festispec.FestispecContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            if (context.Categories.Count() == 0)
            {
                context.Categories.AddOrUpdate(
                    new Category() { Name = "Syntax" },
                    new Category() { Name = "Structuur" },
                    new Category() { Name = "Java" },
                    new Category() { Name = "Python" },
                    new Category() { Name = "C#" }
                );
            }

            context.Answers.RemoveRange(context.Answers);
            context.Questions.RemoveRange(context.Questions);

            Question q1;
            Question q2;
            Question q3;
            Question q4;
            Question q5;

            context.Questions.AddOrUpdate(
                q1 = new Question()
                {
                    Content = "Hoe is x?",
                    CategoryId = 2,
                    Answers = {
                        new Answer(){ Content = "A", Correct = false },
                        new Answer(){ Content = "B", Correct = false },
                        new Answer(){ Content = "C", Correct = true },
                        new Answer(){ Content = "D", Correct = false }
                    }
                },
                q2 = new Question()
                {
                    Content = "Wat is y?",
                    CategoryId = 3,
                    Answers = {
                        new Answer(){ Content = "1", Correct = false },
                        new Answer(){ Content = "2", Correct = true },
                        new Answer(){ Content = "3", Correct = false },
                        new Answer(){ Content = "4", Correct = false }
                    }
                },
                q3 = new Question()
                {
                    Content = "Hoe vind je z?",
                    CategoryId = 4,
                    Answers = {
                        new Answer(){ Content = "1ste", Correct = true },
                        new Answer(){ Content = "2de", Correct = false },
                        new Answer(){ Content = "3de", Correct = false },
                        new Answer(){ Content = "4de", Correct = false }
                    }
                },
                q4 = new Question()
                {
                    Content = "Wat is er fout aan k?",
                    CategoryId = 1,
                    Answers = {
                        new Answer(){ Content = "alfa", Correct = false },
                        new Answer(){ Content = "beta", Correct = false },
                        new Answer(){ Content = "gamma", Correct = false },
                        new Answer(){ Content = "delta", Correct = true }
                    }
                },
                q5 = new Question()
                {
                    Content = "Wat bedoelt men met c?",
                    CategoryId = 5,
                    Answers = {
                        new Answer(){ Content = "I", Correct = false },
                        new Answer(){ Content = "II", Correct = true },
                        new Answer(){ Content = "III", Correct = false },
                        new Answer(){ Content = "IV", Correct = false }
                    }
                }
            );

            context.Quizzes.RemoveRange(context.Quizzes);

            context.Quizzes.AddOrUpdate(
                new Quiz() { Name = "TestQuiz1", Questionnaire = { q1, q3 } },
                new Quiz() { Name = "TestQuiz2", Questionnaire = { q4, q5, q2 } }
            );
        }
    }
}
