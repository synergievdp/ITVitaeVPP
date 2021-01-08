using System;
using System.Windows.Controls;

namespace MorseCodeApp {
    public class ViewModel : Notify {
        public MorseCode MorseCode { get; set; }
        private string _input = "ABCDEFGHIJKLMNOPQRSTUVWXYZ 0123456789 .,?!-/;\'()=@&";
        public string Input { get { return _input; } set { _input = value; OnPropertyChanged(); } }
        private string _output;
        public string Output { get { return _output; } set { _output = value; OnPropertyChanged(); } }

        public ViewModel() {
            MorseCode = new MorseCode();
            Output = MorseCode.ToMorse(Input);
        }
        public void OnClick(object sender, EventArgs e) {
            switch((sender as Button).Name) {
                case "Morse":
                    Output = MorseCode.ToMorse(Input);
                    break;
                case "Alpha":
                    Output = MorseCode.ToAlpha(Input);
                    break;
                case "Copy":
                    System.Windows.Clipboard.SetText(Output);
                    break;
            }
        }
    }
}
