namespace Festispec
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class FestispecContext : DbContext
    {
        public FestispecContext()
            : base("name=FestispecDB")
        {
            Database.SetInitializer<FestispecContext>(new CreateDatabaseIfNotExists<FestispecContext>());
        }

        public virtual DbSet<Answer> Answers { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Question> Questions { get; set; }
        public virtual DbSet<Quiz> Quizzes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            /*modelBuilder.Entity<Category>()
                .Property(e => e.Naam)
                .IsFixedLength();

            modelBuilder.Entity<Question>()
                .HasMany(e => e.Answers)
                .WithRequired(e => e.Question)
                .WillCascadeOnDelete(false);*/
        }
    }
}
