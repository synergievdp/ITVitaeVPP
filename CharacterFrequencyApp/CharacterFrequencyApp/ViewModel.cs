using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Windows.Controls;

namespace CharacterFrequencyApp {
    public class ViewModel : Notify {
        public CharacterFrequency CharacterFrequency;
        public FileHandler FileHandler;
        private string _input = "Put text here.";

        public string Input { get { return _input; } set { _input = value; OnPropertyChanged(); } }
        private Dictionary<char, int> _freqs;
        public Dictionary<char, int> Freqs { get { return _freqs; } set { _freqs = value; OnPropertyChanged(); } }

        public ViewModel() {
            CharacterFrequency = new CharacterFrequency();
            FileHandler = new FileHandler();
        }

        public void OnClick(object sender, EventArgs e) {
            switch ((sender as Button).Name) {
                case "Count":
                    if (Input.Length > 0)
                        Freqs = CharacterFrequency.Count(Input);
                    else Input = "Put text here.";
                    break;
                case "Open":
                    OpenFileDialog openFileDialog = new OpenFileDialog();
                    if (openFileDialog.ShowDialog() == true) {
                        Input = FileHandler.Load(openFileDialog.FileName);
                    }
                    break;
            }
        }
    }
}
