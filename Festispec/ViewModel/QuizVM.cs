using System.Collections.ObjectModel;
using System.Linq;

namespace Festispec.ViewModel
{
    public class QuizVM
    {
        private Quiz _q;

        public QuizVM(Quiz q)
        {
            _q = q;
        }

        public int Id
        {
            get { return _q.Id; }
        }

        public string Name
        {
            get { return _q.Name; }
            set
            {
                _q.Name = value;
            }
        }

        public ObservableCollection<QuestionVM> Questionnaire
        {
            get
            {
                return new ObservableCollection<QuestionVM>(_q.Questionnaire.Select(q => new QuestionVM(q)));
            }
        }

        public int QuestionCount
        {
            get
            {
                return _q.Questionnaire.Count;
            }
        }

        public Quiz ToModel
        {
            get { return _q; }
        }
    }
}
