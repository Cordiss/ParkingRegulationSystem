using Diploma.Factory;
using Diploma.ViewModels;

namespace Diploma
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow //: Window
    {
        public MainWindow()
        {
            InitializeComponent();

            OnLoad();
        }

        private void OnLoad()
        {
            IViewModelFactory viewModelFactory = new ViewModelFactory();
            DataContext = (MainWindowViewModel) viewModelFactory.CreateViewModelByType(typeof(MainWindowViewModel));
        }
    }
}