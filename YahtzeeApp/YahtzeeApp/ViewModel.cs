using System;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace YahtzeeApp {
    public class ViewModel : Notify {
        public Yahtzee Yahtzee { get; set; }
        public int[] PlayersBox { get; set; } = { 2, 3, 4 };
        public int Players { get; set; } = 2;
        public ObservableCollection<string> ScoreCards { get; set; }
        private int _columns;
        public int Columns { get { return _columns; } set { _columns = value; OnPropertyChanged(); } }

        private string _message;
        public string Message { get { return _message; } set { _message = value; OnPropertyChanged(); } }

        public ViewModel() {
            ScoreCards = new ObservableCollection<string>();
            Reset(Players);
        }

        public void Reset(int players) {
            Yahtzee = new Yahtzee(players);
            Columns = players + 1;
            ScoreCard();
            Message = String.Format("Player {0}'s turn. Click Roll Dice or Skip Turn.", Yahtzee.CurrentPlayer.index);
        }

        public void ScoreCard() {
            ScoreCards.Clear();

            foreach (string category in Player.categories) {
                ScoreCards.Add(category);
                foreach (Player player in Yahtzee.Players) {
                    ScoreCards.Add(player.score[category].ToString());
                }
            }
        }

        public void OnStart(object sender, EventArgs e) {
            Reset(Players);
        }

        public void OnPlayers(object sender, EventArgs e) {
            Players = (int)(sender as ComboBox).SelectedItem;
        }

        public void OnRoll(object sender, EventArgs e) {
            if (Yahtzee.Rolls >= 3) Message = "Please select a scoring category.";
            else Yahtzee.Roll();
        }

        public void OnSkip(object sender, EventArgs e) {
            Yahtzee.Rolls = 3;
        }

        public void OnScore(object sender, EventArgs e) {
            if (Yahtzee.Rolls >= 3) {
                UniformGrid grid = sender as UniformGrid;
                MouseEventArgs mouse = e as MouseEventArgs;

                int index = (int)(mouse.GetPosition(grid).Y / (grid.ActualHeight / Player.categories.Length));

                Yahtzee.Score(Player.categories[index]);
                ScoreCard();
                Message = String.Format("Player {0}'s turn. Click Roll Dice or Skip Turn.", Yahtzee.CurrentPlayer.index);
            }
        }
    }
}
