namespace Festispec
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Answer
    {
        [Key]
        public int Id { get; set; }

        [Column(TypeName = "ntext")]
        [Required]
        public string Content { get; set; }

        [Required]
        public int QuestionId { get; set; }

        public virtual Question Question { get; set; }
    }
}
