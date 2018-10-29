using System.Collections.ObjectModel;
using System.Linq;

namespace Festispec.ViewModel
{
    public class QuestionnaireVM
    {
        private Questionnaire _q;

        public QuestionnaireVM(Questionnaire q)
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
            set { _q.Name = value; }
        }

        public int InspectionId
        {
            get { return _q.InspectionId; }
            set { _q.InspectionId = value; }
        }

        public string Instructions {
            get { return _q.Instructions; }
            set { _q.Instructions = value; }
        }

        public ObservableCollection<QuestionVM> QuestionList
        {
            get
            {
                return new ObservableCollection<QuestionVM>(_q.QuestionList.Select(q => new QuestionVM(q)));
            }
        }

        public int QuestionCount
        {
            get
            {
                return _q.QuestionList.Count;
            }
        }

        public Questionnaire ToModel
        {
            get { return _q; }
        }
    }
}
