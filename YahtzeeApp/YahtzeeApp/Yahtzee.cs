using System.Collections.ObjectModel;

namespace YahtzeeApp {
    public class Yahtzee : Notify {
        public ObservableCollection<Dice> Dice { get; set; }
        public ObservableCollection<Player> Players { get; set; }
        private Player _currentPlayer;
        public Player CurrentPlayer { get { return _currentPlayer; } set { _currentPlayer = value; OnPropertyChanged(); } }
        private int _rolls = 0;
        public int Rolls { get { return _rolls; } set { _rolls = value; OnPropertyChanged(); } }

        public Yahtzee(int maxPlayers) {
            Reset(maxPlayers);
        }

        public void Reset(int maxPlayers) {
            Players = new ObservableCollection<Player>();
            for (int i = 1; i <= maxPlayers; i++) {
                Players.Add(new Player(i));
            }
            CurrentPlayer = Players[0];

            Dice = new ObservableCollection<Dice>();
            for (int i = 0; i < 5; i++) Dice.Add(new Dice(1));
            Roll();
        }

        public void Roll() {
            if (Rolls < 3) {
                foreach (Dice die in Dice) die.Roll();
                Rolls++;
            }
        }

        public void Score(string category) {
            CurrentPlayer.Score(Dice, category);
            NextTurn();
        }

        public void NextTurn() {
            int current = CurrentPlayer.index;
            CurrentPlayer = current == Players.Count ? Players[0] : Players[current];
            Rolls = 0;
            foreach (Dice die in Dice) die.KeepValue = false;
            Roll();
        }
    }
}
