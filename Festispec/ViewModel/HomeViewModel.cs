using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using Festispec.Views;
using System;
using System.Windows.Input;

namespace Festispec.ViewModel
{
    public class HomeViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>

        public HomeViewModel()
        {
            ShowQuizManagementCommand = new RelayCommand(ShowQuizManagement);
        }
        public ICommand ShowQuizManagementCommand { get; set; }
        
        public void ShowQuizManagement()
        {
            CommonServiceLocator.ServiceLocator.Current.GetInstance<QuestionnaireManagementViewModel>().Init();
            new QuizManagement().Show();
        }

    }
}   