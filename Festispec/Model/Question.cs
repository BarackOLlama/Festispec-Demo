namespace Festispec
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Question
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Question()
        {
            Quizzes = new HashSet<Quiz>();
            Answers = new HashSet<Answer>();
        }

        public Question(string content)
        {
            Quizzes = new HashSet<Quiz>();
            Answers = new HashSet<Answer>();
            Content = content;
        }

        [Key]
        public int Id { get; set; }

        [Column(TypeName = "ntext")]
        [Required]
        public string Content { get; set; }

        public int CategoryId { get; set; }
        
        public virtual ICollection<Quiz> Quizzes { get; set; }

        public virtual ICollection<Answer> Answers { get; set; }

        public virtual Category Category { get; set; }
    }
}
