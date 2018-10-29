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
        private int QuizId;
        public QuizVM Quiz { get; set; }
        public string NewQuestionText { get; set; }
        public ObservableCollection<AnswerVM> Answers { get; set; }
        public ObservableCollection<CategoryVM> Categories { get; set; }

        private QuizVM _BackupQuizState { get; set; }

        private FestispecContext _Context;

        /// <summary>
        /// Initializes a new instance of the QuizManagementViewModel class.
        /// </summary>
        public QuestionnaireCreateViewModel()
        {
            QuizId = -1;
            Messenger.Default.Register<int>(this, "QuizManagementToQuizView", x => QuizId = x);

            // Setup relay commands
            SaveChangesCommand = new RelayCommand<Window>(SaveChanges);
            DiscardChangesCommand = new RelayCommand<Window>(DiscardChanges);
            AddQuestionCommand = new RelayCommand<string>(AddQuestion);
            GetAnswersCommand = new RelayCommand<QuestionVM>(GetAnswers);
            DeleteRowCommand = new RelayCommand<object>(DeleteRow);
        }

        private void CloseAction(Window window)
        {
            _Context.Dispose();
            window.Close();
        }

        private void SaveChanges(Window window)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to save these changes?",
                "Confirm save",
                MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                _Context.SaveChanges();
                CommonServiceLocator.ServiceLocator.Current.GetInstance<QuizManagementViewModel>().Init(_Context);
                CloseAction(window);
            }
        }

        private void DiscardChanges(Window window)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to close without saving?",
                "Confirm discard",
                MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                CloseAction(window);
            }
        }

        public RelayCommand<Window> SaveChangesCommand { get; set; }
        public RelayCommand<Window> DiscardChangesCommand { get; set; }
        public RelayCommand<string> AddQuestionCommand { get; set; }
        public RelayCommand<QuestionVM> GetAnswersCommand { get; set; }
        public RelayCommand<object> DeleteRowCommand { get; set; }

        internal void Init()
        {
            _Context = new FestispecContext();
            if (QuizId != -1)
            {
                Quiz = _Context.Quizzes
                    .Include(nameof(Quiz.Questionnaire))
                    .Where(q => q.Id == QuizId)
                    .ToList()
                    .Select(q => new QuizVM(q))
                    .FirstOrDefault();
            }
            else
            {
                Quiz = new QuizVM(new Quiz());
                _Context.Quizzes.Add(Quiz.ToModel);
            }

            var categories = _Context.Categories
                .ToList()
                .Select(c => new CategoryVM(c));
            Categories = new ObservableCollection<CategoryVM>(categories);
        }

        private void AddQuestion(string questionContent)
        {
            NewQuestionText = "";
            RaisePropertyChanged(nameof(NewQuestionText));
            var newQuestion = new QuestionVM(new Question(questionContent));
            Quiz.Questionnaire.Add(newQuestion);
            _Context.Questions.Add(newQuestion.ToModel);
            RaisePropertyChanged(nameof(Quiz.Questionnaire));
        }

        private void GetAnswers(QuestionVM question)
        {
            if (question != null)
            {
                var answers = _Context.Answers
                    .Where(a => a.QuestionId == question.Id)
                    .ToList()
                    .Select(a => new AnswerVM(a));
                Answers = new ObservableCollection<AnswerVM>(answers);
                RaisePropertyChanged(nameof(Answers));
            }
        }

        private void DeleteRow(object row)
        {
            if (row is QuestionVM question)
            {
                _Context.Questions.Remove(question.ToModel);
                Quiz.Questionnaire.Remove(question);
                RaisePropertyChanged(nameof(Quiz));
            }
            else if (row is AnswerVM answer)
            {
                _Context.Answers.Remove(answer.ToModel);
                Answers.Remove(answer);
            }
        }
    }
}