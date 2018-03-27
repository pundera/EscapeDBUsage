using EscapeDBUsage.UIClasses;
using Prism.Mvvm;
using System.Collections.ObjectModel;

namespace EscapeDBUsage.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private string _title = "Escape DB Usage (Bindings between Excels, Tabs, Values and DB Tables)";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public MainWindowViewModel(MainViewModel mainViewModel)
        {
            MainViewModel = mainViewModel;
        }

        private MainViewModel mainViewModel;
        public MainViewModel MainViewModel
        {
            get
            {
                return mainViewModel;
            }
            set
            {
                SetProperty(ref mainViewModel, value);
            }
        }

    }
}
