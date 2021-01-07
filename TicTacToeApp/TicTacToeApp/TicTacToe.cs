using System.Collections.ObjectModel;

namespace TicTacToeApp {
    public class TicTacToe : Notify {
        public int[][] Grid { get; set; }
        public int maxPlayers = 2;
        private int _player;
        public int Player { get { return _player; } set { _player = value; OnPropertyChanged(); } }
        public int Size { get; set; } = 3;

        public TicTacToe(int size) {
            Grid = new int[Size][];
            for (int i = 0; i < Size; i++)
                Grid[i] = new int[Size];

            Reset();
        }

        public bool NextTurn(int x, int y) {
            if (Grid[y][x] == 0) {
                Grid[y][x] = Player;

                bool check = Check(x, y);
                if (!check) {
                    Player = (Player == 2 ? 1 : 2);
                } else {
                    Reset();
                }

                return true;
            } else return false;
        }

        bool Check(int x, int y) {
            int[,,] directions = new int[8,2,2]{
            { {-2, -2}, {-1, -1} },
            { { -2, 0}, { -1, 0} },
            { { -2, 2}, { -1, 1} },
            { { 0, -2}, { 0, -1} },
            { { 0, 2}, { 0, 1} },
            { { 2, -2}, { 1, -1} },
            { { 2, 0}, { 1, 0} },
            { { 2, 2}, { 1, 1} }
            };

            bool check;
            for(int i = 0; i < directions.GetLength(0); i++) {
                check = true;
                for(int j = 0; j < directions.GetLength(1); j++) {
                    if (y + directions[i, j, 0] >= 0 && y + directions[i, j, 0] < Size &&
                        x + directions[i, j, 1] >= 0 && x + directions[i, j, 1] < Size) {
                        if (Grid[y + directions[i, j, 0]][x + directions[i, j, 1]] != Player) check = false;
                    } else check = false;
                }
                if (check) return check;
            }

            return false;
        }

        void Reset() {
            for(int y = 0; y < Size; y++) {
                for(int x = 0; x < Size; x++) {
                    Grid[y][x] = 0;
                }
            }

            Player = 1;
        }
    }
}
