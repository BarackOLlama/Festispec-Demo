using System.Collections.ObjectModel;
using System.Linq;

namespace Festispec.ViewModel
{
    public class QuestionVM
    {
        private Question _q;

        public QuestionVM(Question q)
        {
            _q = q;
        }

        public int Id
        {
            get { return _q.Id; }
        }

        public string Content
        {
            get { return _q.Content; }
            set { _q.Content = value; }
        }

        public ObservableCollection<AnswerVM> Answers
        {
            get { return new ObservableCollection<AnswerVM>(_q.Answers.Select(a => new AnswerVM(a))); }
        }

        public int AnswerCount
        {
            get { return Answers.Count; }
        }

        public string Category
        {
            get { return _q.QuestionType.Name; }
        }

        public Question ToModel
        {
            get { return _q; }
        }
    }

}
