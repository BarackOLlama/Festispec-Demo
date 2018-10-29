using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Festispec.Views;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace Festispec.ViewModel
{
    public class QuizManagementViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the QuizManagementViewModel class.
        /// </summary>

        protected FestispecContext _Context;

        public ObservableCollection<QuizVM> Quizzes { get; set; }

        private QuizVM _SelectedQuiz;
        public QuizVM SelectedQuiz
        {
            get { return _SelectedQuiz; }
            set
            {
                _SelectedQuiz = value;
                RaisePropertyChanged(nameof(SelectedQuiz));
            }
        }

        public RelayCommand EditQuizCommand { get; set; }
        public RelayCommand NewQuizCommand { get; set; }
        public RelayCommand DeleteRowCommand { get; set; }
        public RelayCommand CanExecuteChangedCommand { get; set; }
        public QuizManagementViewModel()
        {
            // Setup relay commands

            EditQuizCommand = new RelayCommand(EditQuiz, QuizSelected);
            NewQuizCommand = new RelayCommand(NewQuiz);
            DeleteRowCommand = new RelayCommand(DeleteRow, QuizSelected);
            CanExecuteChangedCommand = new RelayCommand(CanExecuteChanged);
        }

        internal void Init(FestispecContext context = null)
        {
            _Context = context ?? new FestispecContext();

            var quizzes = _Context.Quizzes
                .Include(nameof(Quiz.Questionnaire))
                .ToList()
                .Select(q => new QuizVM(q));
            Quizzes = new ObservableCollection<QuizVM>(quizzes);
            RaisePropertyChanged(nameof(Quizzes));
        }

        private bool QuizSelected()
        {
            return SelectedQuiz != null;
        }

        private void CanExecuteChanged()
        {
            DeleteRowCommand.RaiseCanExecuteChanged();
            EditQuizCommand.RaiseCanExecuteChanged();
        }

        private void DeleteRow()
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to remove this quiz?",
                "Confirm delete",
                MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                _Context.Quizzes.Remove(SelectedQuiz.ToModel);
                Quizzes.Remove(SelectedQuiz);
                _Context.SaveChanges();
                SelectedQuiz = null;
            }
        }

        private void EditQuiz()
        {
            Messenger.Default.Send(SelectedQuiz.Id, "QuizManagementToQuizView");

            CommonServiceLocator.ServiceLocator.Current.GetInstance<QuestionnaireCreateViewModel>().Init();
            new QuestionnaireCreate().Show();
        }

        private void NewQuiz()
        {
            Messenger.Default.Send(-1, "QuizManagementToQuizView");

            CommonServiceLocator.ServiceLocator.Current.GetInstance<QuestionnaireCreateViewModel>().Init();
            new QuestionnaireCreate().Show();
        }
    }
}