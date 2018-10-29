namespace Festispec.ViewModel
{
    public class AnswerVM
    {
        private Answer _answer;

        public AnswerVM(Answer a)
        {
            _answer = a;
        }

        public string Content
        {
            get { return _answer.Content; }
            set { _answer.Content = value; }
        }

        public Answer ToModel
        {
            get { return _answer; }
        }
    }
}
