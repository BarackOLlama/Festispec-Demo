using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using Festispec.Views;
using System;
using System.Windows;
using System.Windows.Input;

namespace Festispec.ViewModel
{
    public class ShowQuizzesToPlayViewModel : QuizManagementViewModel
    {
        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public ShowQuizzesToPlayViewModel() : base()
        {
            this.Init();

            Messenger.Default.Register<FestispecContext>(this, "SendContext", c => _Context = c);

            CloseWindowCommand = new RelayCommand(CloseWindow);
            ShowQuizManagementCommand = new RelayCommand(ShowQuizManagement);
            WindowDeactivatedCommand = new RelayCommand<object>(WindowDeactivated);
        }

        public ICommand CloseWindowCommand { get; set; }
        public ICommand ShowQuizManagementCommand { get; set; }
        public ICommand WindowDeactivatedCommand { get; set; }

        public void CloseWindow()
        {
        }

        public void ShowQuizManagement()
        {
            new QuizManagement().Show();
        }

        public void WindowDeactivated(object sender)
        {
            Window window = (Window)sender;
            window.Topmost = true;
            window.Activate();
        }

    }
}   