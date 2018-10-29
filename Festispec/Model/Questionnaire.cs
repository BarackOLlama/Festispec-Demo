using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festispec
{
    public partial class Questionnaire
    {
        public Questionnaire()
        {
            QuestionList = new HashSet<Question>();
        }

        public Questionnaire(string n, ICollection<Question> qs)
        {
            this.Name = n;
            this.QuestionList = qs;
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public ICollection<Question> QuestionList { get; set; }
    }
}
