using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Festispec.Views;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace Festispec.ViewModel
{
    public class QuestionnaireManagementViewModel : ViewModelBase
    {
        protected FestispecContext _Context;

        public ObservableCollection<QuestionnaireVM> Questionnnaires { get; set; }

        private QuestionnaireVM _SelectedQuestionnaire;
        public virtual QuestionnaireVM SelectedQuestionnaire
        {
            get { return _SelectedQuestionnaire; }
            set
            {
                _SelectedQuestionnaire = value;
                RaisePropertyChanged(nameof(SelectedQuestionnaire));
            }
        }

        public RelayCommand EditQuestionnaireCommand { get; set; }
        public RelayCommand NewQuestionnaireCommand { get; set; }
        public RelayCommand DeleteRowCommand { get; set; }
        public RelayCommand CanExecuteChangedCommand { get; set; }
        public QuestionnaireManagementViewModel()
        {
            // Setup relay commands
            EditQuestionnaireCommand = new RelayCommand(EditQuestionnaire, QuestionnaireSelected);
            NewQuestionnaireCommand = new RelayCommand(NewQuestionnaire);
            DeleteRowCommand = new RelayCommand(DeleteRow, QuestionnaireSelected);
            CanExecuteChangedCommand = new RelayCommand(CanExecuteChanged);
        }

        public virtual void Init(FestispecContext context = null)
        {
            _Context = context ?? new FestispecContext();

            var quizzes = _Context.Questionnaires
                .Include(nameof(Questionnaire.QuestionList))
                .ToList()
                .Select(q => new QuestionnaireVM(q));
            Questionnnaires = new ObservableCollection<QuestionnaireVM>(quizzes);
            RaisePropertyChanged(nameof(Questionnnaires));
        }

        public bool QuestionnaireSelected()
        {
            return SelectedQuestionnaire != null;
        }

        private void CanExecuteChanged()
        {
            DeleteRowCommand.RaiseCanExecuteChanged();
            EditQuestionnaireCommand.RaiseCanExecuteChanged();
        }

        private void DeleteRow()
        {
            MessageBoxResult result = MessageBox.Show("Weet u zeker dat u deze vragenlijst wilt verwijderen?",
                "Verwijdering bevestigen",
                MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                _Context.Questionnaires.Remove(SelectedQuestionnaire.ToModel);
                Questionnnaires.Remove(SelectedQuestionnaire);
                _Context.SaveChanges();
                Messenger.Default.Send(_Context, "SendContext");
                SelectedQuestionnaire = null;
            }
        }

        private void EditQuestionnaire()
        {
            Messenger.Default.Send(SelectedQuestionnaire.Id, "QuizManagementToQuizView");

            CommonServiceLocator.ServiceLocator.Current.GetInstance<QuestionnaireCreateViewModel>().Init();
            new QuestionnaireCreate().Show();
        }

        private void NewQuestionnaire()
        {
            Messenger.Default.Send(-1, "QuizManagementToQuizView");

            CommonServiceLocator.ServiceLocator.Current.GetInstance<QuestionnaireCreateViewModel>().Init();
            new QuestionnaireCreate().Show();
        }
    }
}