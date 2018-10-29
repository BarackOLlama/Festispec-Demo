using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festispec
{
    public partial class Quiz
    {
        public Quiz()
        {
            Questionnaire = new HashSet<Question>();
        }

        public Quiz(string n, ICollection<Question> qs)
        {
            this.Name = n;
            this.Questionnaire = qs;
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public ICollection<Question> Questionnaire { get; set; }
    }
}
