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

        public ObservableCollection<Answer> Answers
        {
            get { return new ObservableCollection<Answer>(_q.Answers.Select(a => a)); }
        }

        public int AnswerCount
        {
            get { return Answers.Count; }
        }

        public Question ToModel
        {
            get { return _q; }
        }
    }

}
