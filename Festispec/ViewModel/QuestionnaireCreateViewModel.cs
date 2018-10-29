using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;


namespace Festispec.ViewModel
{
    public class QuestionnaireCreateViewModel : ViewModelBase
    {
        private int QuestionnaireId;
        private QuestionnaireVM _Questionnaire;
        public QuestionnaireVM Questionnaire
        {
            get { return _Questionnaire; }
            set { _Questionnaire = value ?? _Questionnaire; }
        }
        public string QuestionnaireName
        {
            get { return _Questionnaire.Name; }
            set { _Questionnaire.Name = value; }
        }
        public string NewQuestionText { get; set; }
        public string NewAnswerText { get; set; }
        public QuestionTypeVM SelectedType { get; set; }
        public QuestionVM SelectedQuestion { get; set; }
        public string WarningText { get; set; }
        public ObservableCollection<AnswerVM> Answers { get; set; }
        public ObservableCollection<QuestionTypeVM> QuestionTypes { get; set; }

        private QuestionnaireVM _BackupQuestionnaireState { get; set; }

        private FestispecContext _Context;

        public RelayCommand<Window> SaveChangesCommand { get; set; }
        public RelayCommand<Window> DiscardChangesCommand { get; set; }
        public RelayCommand AddQuestionCommand { get; set; }
        public RelayCommand AddAnswerCommand { get; set; }
        public RelayCommand GetAnswersCommand { get; set; }
        public RelayCommand<object> DeleteRowCommand { get; set; }

        /// <summary>
        /// Initializes a new instance of the QuizManagementViewModel class.
        /// </summary>
        public QuestionnaireCreateViewModel()
        {
            QuestionnaireId = -1;
            Messenger.Default.Register<int>(this, "QuizManagementToQuizView", x => QuestionnaireId = x);

            // Setup relay commands
            SaveChangesCommand = new RelayCommand<Window>(SaveChanges, CanSave);
            DiscardChangesCommand = new RelayCommand<Window>(DiscardChanges);
            AddQuestionCommand = new RelayCommand(AddQuestion);
            AddAnswerCommand = new RelayCommand(AddAnswer, CanAddAnswer);
            GetAnswersCommand = new RelayCommand(GetAnswers);
            DeleteRowCommand = new RelayCommand<object>(DeleteRow);
        }

        internal void Init()
        {
            _Context = new FestispecContext();
            if (QuestionnaireId != -1)
            {
                Questionnaire = _Context.Questionnaires
                    .Include(nameof(Questionnaire.QuestionList))
                    .Where(q => q.Id == QuestionnaireId)
                    .ToList()
                    .Select(q => new QuestionnaireVM(q))
                    .FirstOrDefault();
                CanExecuteChanged();
            }
            else
            {
                Questionnaire = new QuestionnaireVM(new Questionnaire());
                _Context.Questionnaires.Add(Questionnaire.ToModel);
                Answers = new ObservableCollection<AnswerVM>();
                CanExecuteChanged();
            }

            var questiontypes = _Context.QuestionTypes
                .ToList()
                .Select(c => new QuestionTypeVM(c));
            QuestionTypes = new ObservableCollection<QuestionTypeVM>(questiontypes);
        }

        private void CanExecuteChanged()
        {
            SaveChangesCommand.RaiseCanExecuteChanged();
            AddAnswerCommand.RaiseCanExecuteChanged();
            RaisePropertyChanged(nameof(SelectedQuestion));
        }

        private void CloseAction(Window window)
        {
            SelectedQuestion = null;
            NewQuestionText = "";
            NewAnswerText = "";
            window.Close();
        }

        private void SaveChanges(Window window)
        {
            MessageBoxResult result = MessageBox.Show("Weet u zeker dat u wilt opslaan?",
                "Opslaan bevestigen",
                MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                _Context.SaveChanges();
                CommonServiceLocator.ServiceLocator.Current.GetInstance<QuestionnaireManagementViewModel>().Init(_Context);
                Messenger.Default.Send(_Context, "SendContext");
                CloseAction(window);
            }
        }

        private bool CanSave(Window args)
        {
            var questionList = new ObservableCollection<QuestionVM>(Questionnaire.QuestionList);
            if (questionList.Count() < 1)
            {
                WarningText = "De vragenlijst moet minstens één vraag hebben";
                RaisePropertyChanged(nameof(WarningText));
                return false;
            }
            foreach (QuestionVM q in questionList)
            {
                if (q.Answers.Count < 2)
                {
                    WarningText = "Multiple choice-vragen moeten minstens twee opties hebben";
                    RaisePropertyChanged(nameof(WarningText));
                    return false;
                }
            }
            WarningText = "";
            RaisePropertyChanged(nameof(WarningText));
            return true;
        }

        private void DiscardChanges(Window window)
        {
            MessageBoxResult result = MessageBox.Show("Weet u zeker dat u wilt afsluiten zonder op te slaan?",
                "Afsluiten bevestigen",
                MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                _Context.Dispose();
                CloseAction(window);
            }
        }

        private void AddQuestion()
        {
            var questionType = _Context.QuestionTypes.FirstOrDefault(c => c.Id == SelectedType.Id);
            var newQuestion = new QuestionVM(new Question
            {
                Content = NewQuestionText,
                QuestionType = questionType
            });

            _Context.Questions.Add(newQuestion.ToModel);
            
            Questionnaire.QuestionList.Add(newQuestion);
            Questionnaire.ToModel.QuestionList.Add(newQuestion.ToModel);
            RaisePropertyChanged(nameof(Questionnaire));            

            NewQuestionText = "";
            RaisePropertyChanged(nameof(NewQuestionText));

            CanExecuteChanged();
        }

        private void AddAnswer()
        {
            var newAnswer = new AnswerVM(new Answer
            {
                Content = NewAnswerText,
                Question = SelectedQuestion.ToModel
            });

            _Context.Answers.Add(newAnswer.ToModel);

            SelectedQuestion.ToModel.Answers.Add(newAnswer.ToModel);
            SelectedQuestion.Answers.Add(newAnswer);
            Answers.Add(newAnswer);
            var temp = SelectedQuestion;
            RaisePropertyChanged(nameof(Questionnaire));
            SelectedQuestion = Questionnaire.QuestionList.FirstOrDefault(q => q.Id == temp.Id);

            NewAnswerText = "";
            RaisePropertyChanged(nameof(NewAnswerText));

            GetAnswers();
            CanExecuteChanged();
        }

        private bool CanAddAnswer()
        {
            return SelectedQuestion != null;
        }

        private void GetAnswers()
        {
            if (SelectedQuestion != null)
            {
                //var answers = _Context.Answers
                //    .Where(a => a.QuestionId == SelectedQuestion.Id)
                //    .ToList()
                //    .Select(a => new AnswerVM(a));
                // Answers = new ObservableCollection<AnswerVM>(answers);
                Answers = SelectedQuestion.Answers;
                RaisePropertyChanged(nameof(Answers));
            }
            CanExecuteChanged();
        }

        private void DeleteRow(object row)
        {
            if (row is QuestionVM question)
            {
                //_Context.Questions.Remove(question.ToModel);
                Questionnaire.QuestionList.Remove(Questionnaire.QuestionList.Where(q => q.Id == question.Id).Single());
                Questionnaire.ToModel.QuestionList.Remove(question.ToModel);
                RaisePropertyChanged(nameof(Questionnaire));
                //AllQuestions.Remove(AllQuestions.Where(q=>q.Id == question.Id).Single());
                //RaisePropertyChanged(nameof(AllQuestions));
                Answers.Clear();
            }
            else if (row is AnswerVM answer)
            {
                _Context.Answers.Remove(answer.ToModel);
                Answers.Remove(answer);
                RaisePropertyChanged(nameof(Questionnaire));
            }

            CanExecuteChanged();
        }
    }
}