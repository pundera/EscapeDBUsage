using Prism.Mvvm;

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

        public MainWindowViewModel()
        {

        }
    }
}
