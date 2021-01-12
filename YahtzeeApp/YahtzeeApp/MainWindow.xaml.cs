using System;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace YahtzeeApp {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        ViewModel ViewModel { get; set; }
        public MainWindow() {
            InitializeComponent();

            ViewModel = new ViewModel();
            DataContext = ViewModel;
        }

        public void OnStart(object sender, EventArgs e) {
            ViewModel.OnStart(sender, e);
        }

        public void OnPlayers(object sender, EventArgs e) {
            ViewModel.OnPlayers(sender, e);
        }

        public void OnRoll(object sender, EventArgs e) {
            ViewModel.OnRoll(sender, e);
        }

        public void OnSkip(object sender, EventArgs e) {
            ViewModel.OnSkip(sender, e);
        }

        public void OnScore(object sender, EventArgs e) {
            ViewModel.OnScore(sender, e);
        }
    }
}
