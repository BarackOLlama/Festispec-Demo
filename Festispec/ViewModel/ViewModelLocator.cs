/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:Festispec"
                           x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/

using CommonServiceLocator;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;

namespace Festispec.ViewModel
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            ////if (ViewModelBase.IsInDesignModeStatic)
            ////{
            ////    // Create design time view services and models
            ////    SimpleIoc.Default.Register<IDataService, DesignDataService>();
            ////}
            ////else
            ////{
            ////    // Create run time view services and models
            ////    SimpleIoc.Default.Register<IDataService, DataService>();
            ////}

            SimpleIoc.Default.Register<HomeViewModel>();
            SimpleIoc.Default.Register<QuizManagementViewModel>(true);
            SimpleIoc.Default.Register<QuestionnaireCreateViewModel>(true);
            SimpleIoc.Default.Register<ShowQuizzesToPlayViewModel>(true);
        }

        public HomeViewModel Main
        {
            get
            {
                return ServiceLocator.Current.GetInstance<HomeViewModel>();
            }
        }

        public ShowQuizzesToPlayViewModel PlayViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ShowQuizzesToPlayViewModel>();
            }
        }

        public QuizManagementViewModel QuizManagement
        {
            get
            {
                return ServiceLocator.Current.GetInstance<QuizManagementViewModel>();
            }
        }

        public QuestionnaireCreateViewModel QuestionnaireCreate
        {
            get
            {
                return ServiceLocator.Current.GetInstance<QuestionnaireCreateViewModel>();
            }
        }

        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}