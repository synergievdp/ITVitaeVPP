using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Controls;
using System.Windows.Input;

namespace TicTacToeApp {
    class ViewModel : Notify {
        private string _message;
        public String Message { get { return _message; } set { _message = value; OnPropertyChanged(); } }
        public TicTacToe TicTacToe { get; set; }
        public ObservableCollection<string> Grid { get; set; }


        public ViewModel() {
            TicTacToe = new TicTacToe(3);
            Grid = new ObservableCollection<string>();
            UpdateGrid();
            Message = String.Format("It's player {0}'s turn.", TicTacToe.Player);
        }

        void UpdateGrid() {
            Grid.Clear();
            int[][] grid = TicTacToe.Grid;
            for(int y = 0; y < grid.Length; y++) {
                for(int x = 0; x < grid[y].Length; x++) {
                    switch(grid[y][x]) {
                        case 0: Grid.Add("-"); break;
                        case 1: Grid.Add("X"); break;
                        case 2: Grid.Add("0"); break;
                    }
                }
            }
        }

        public void OnClick(object sender, MouseEventArgs e) {
            Panel panel = sender as Panel;
            int x = (int)(e.GetPosition(panel).X / (panel.ActualWidth / TicTacToe.Size));
            int y = (int)(e.GetPosition(panel).Y / (panel.ActualHeight / TicTacToe.Size));

            bool nextTurn = TicTacToe.NextTurn(x, y);
            if (!nextTurn) {
                Message = String.Format("Pick another spot, player {0}", TicTacToe.Player);
            } else {
                Message = String.Format("It's player {0}'s turn.", TicTacToe.Player);
            }

            UpdateGrid();
        }
    }
}
