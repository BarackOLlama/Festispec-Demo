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

        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public int InspectionId { get; set; }

        public string Instructions { get; set; }

        public ICollection<Question> QuestionList { get; set; }
    }
}
