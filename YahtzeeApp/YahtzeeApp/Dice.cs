using System;

namespace YahtzeeApp {
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
