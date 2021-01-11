using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
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
    }

    public class ViewModel : Notify {
        public Yahtzee Yahtzee { get; set; }
        public int[] PlayersBox { get; set; } = { 2, 3, 4 };
        public int Players { get; set; } = 2;

        public ViewModel() {
            Reset(Players);
        }

        public void Reset(int players) {
            Yahtzee = new Yahtzee(players);
        }

        public void OnStart(object sender, EventArgs e) {
            Reset(Players);
        }

        public void OnPlayers(object sender, EventArgs e) {
            Players = (int)(sender as ComboBox).SelectedItem;
        }

        public void OnRoll(object sender, EventArgs e) {
            Yahtzee.Roll();
        }

        public void OnSkip(object sender, EventArgs e) {
            Yahtzee.NextTurn();
        }
    }

    public class Notify : INotifyPropertyChanged {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string name = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }

    public class Yahtzee : Notify {
        public ObservableCollection<Dice> Dice { get; set; }
        private int _currentPlayer = 1;
        public int CurrentPlayer { get { return _currentPlayer; } set { _currentPlayer = value; OnPropertyChanged(); } }
        private int _rolls = 1;
        public int Rolls { get { return _rolls; } set { _rolls = value; OnPropertyChanged(); } }
        public int maxPlayers = 4;

        public Yahtzee(int maxPlayers) {
            this.maxPlayers = maxPlayers;
            Dice = new ObservableCollection<Dice>() {
                new Dice(1), new Dice(1), new Dice(1), new Dice(1), new Dice(1)
            };
        }

        public void Roll() {
            foreach (Dice die in Dice) die.Roll();
            Rolls++;
            if (Rolls > 3) NextTurn();
        }

        public void NextTurn() {
            CurrentPlayer = CurrentPlayer == maxPlayers ? 1 : CurrentPlayer+1;
            Rolls = 1;
            foreach (Dice die in Dice) die.KeepValue = false;
        }
    }

    public class Player {
        static public string[] categories = { 
            "ACES", "TWOS", "THREES", "FOURS", "FIVES", "SIXES",
            "THREEOFAKIND", "FOUROFAKIND", "FULLHOUSE",
            "SMALLSTRAIGHT", "LARGESTRAIGHT", "YAHTZEE", "CHANCE"
        };
        Dictionary<string, int> score;

        public Player() {
            score = new Dictionary<string, int>();
            foreach(string category in categories) {
                score.Add(category, -1);
            }
        }
    }

    public class Dice : Notify {
        public Random random = new Random();
        private int _value;
        public int Value { get { return _value; } set { _value = value; OnPropertyChanged(); } }
        private bool _keepValue = false;
        public bool KeepValue { get { return _keepValue; } set { _keepValue = value; OnPropertyChanged(); } }

        public Dice(int value) {
            Value = value;
        }

        public void Roll() {
            if(!KeepValue) Value = random.Next(6) + 1;
        }
    }
}
