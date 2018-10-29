using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festispec.ViewModel
{
    public class QuestionTypeVM
    {
        private QuestionType _questionType;

        public QuestionTypeVM(QuestionType c)
        {
            _questionType = c;
        }

        public int Id
        {
            get { return _questionType.Id; }
        }

        public string Name
        {
            get { return _questionType.Name; }
        }
    }
}
