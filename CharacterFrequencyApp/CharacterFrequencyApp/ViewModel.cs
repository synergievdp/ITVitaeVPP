using System;
using System.Collections.Generic;

namespace CharacterFrequencyApp {
    public class ViewModel : Notify {
        public CharacterFrequency CharacterFrequency;
        private string _input = "Put text here.";

        public string Input { get { return _input; } set { _input = value; OnPropertyChanged(); } }
        private Dictionary<char, int> _freqs;
        public Dictionary<char, int> Freqs { get { return _freqs; } set { _freqs = value; OnPropertyChanged(); } }

        public ViewModel() {
            CharacterFrequency = new CharacterFrequency();
        }

        public void OnClick(object sender, EventArgs e) {
            if (Input.Length > 0)
                Freqs = CharacterFrequency.Count(Input);
            else Input = "Put text here.";
        }
    }
}
