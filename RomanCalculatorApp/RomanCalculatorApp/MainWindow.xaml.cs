using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RomanCalculatorApp {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        ViewModel ViewModel;

        public MainWindow() {
            InitializeComponent();

            ViewModel = new ViewModel();
            DataContext = ViewModel;
        }

        public void OnClick(object sender, EventArgs e) {
            ViewModel.OnClick(sender, e);
        }
    }

    public class ViewModel : Notify {
        public List<char> Buttons { get; set; } = new List<char>();

        private string _input = "N";
        public string Input { get { return _input; } set { _input = value; OnPropertyChanged(); } }
        private string _output = "N";
        public string Output { get { return _output; } set { _output = value; OnPropertyChanged(); } }
        private string _operation = "N + N =";
        public string Operation { get { return _operation; } set { _operation = value; OnPropertyChanged(); } }

        public ViewModel() {
            Buttons.AddRange(RomanNumerals.Numerals.Keys);
            Buttons.Add('+');
            Buttons.Add('-');
            Buttons.Add('/');
            Buttons.Add('x');
        }

        public void OnClick(object sender, EventArgs e) {
            string button = (sender as Button).Content.ToString();
            int value = 0;

            switch(button) {
                case "+":
                    value = RomanNumerals.GetValue(Output) + RomanNumerals.GetValue(Input);
                    Operation = String.Join(" ", Output, "+", Input, "=");
                    Output = RomanNumerals.GetSymbol(value);
                    Input = "N";
                    break;
                case "-":
                    value = RomanNumerals.GetValue(Output) - RomanNumerals.GetValue(Input);
                    Operation = String.Join(" ", Output, "-", Input, "=");
                    Output = RomanNumerals.GetSymbol(value);
                    Input = "N";
                    break;
                case "/":
                    value = RomanNumerals.GetValue(Output) / RomanNumerals.GetValue(Input);
                    Operation = String.Join(" ", Output, "/", Input, "=");
                    Output = RomanNumerals.GetSymbol(value);
                    Input = "N";
                    break;
                case "x":
                    value = RomanNumerals.GetValue(Output) * RomanNumerals.GetValue(Input);
                    Operation = String.Join(" ", Output, "*", Input, "=");
                    Output = RomanNumerals.GetSymbol(value);
                    Input = "N";
                    break;
                case "N":
                    if (Input.Equals("N")) Output = "N";
                    else Input = "N";
                    break;
                default:
                    value = RomanNumerals.GetValue(Input) + RomanNumerals.Numerals[button[0]];
                    Input = RomanNumerals.GetSymbol(value);
                    break;
            }
        }
    }

    public class Notify : INotifyPropertyChanged {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string name = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }

    public class RomanNumerals {
        static public Dictionary<char, int> Numerals = new Dictionary<char, int>() {
            {'M', 1000 },
            {'D', 500 },
            {'C', 100 },
            {'L', 50 },
            {'X', 10 },
            {'V', 5 },
            {'I', 1 },
            {'N', 0 },
        };

        static public string GetSymbol(int value) {
            if (value == 0) return "N";

            IEnumerable<char> keys = Numerals.Keys;
            StringBuilder sb = new StringBuilder();

            for(int i = 0; i < keys.Count() - 1; i++) {
                int amount = value / Numerals[keys.ElementAt(i)];
                value %= Numerals[keys.ElementAt(i)];
                for (int a = 0; a < amount; a++) sb.Append(keys.ElementAt(i));
            }

            return sb.ToString();
        }

        static public int GetValue(string symbols) {
            int value = 0;
            Dictionary<char, int> symbolsDict = new Dictionary<char, int>();

            foreach(char symbol in symbols) {
                if (symbolsDict.ContainsKey(symbol)) symbolsDict[symbol]++;
                else symbolsDict.Add(symbol, 1);
            }

            foreach(char symbol in symbolsDict.Keys) {
                value += Numerals[symbol] * symbolsDict[symbol];
            }

            return value;
        }
    }
}
