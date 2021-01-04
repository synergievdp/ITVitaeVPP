using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace CalculatorApp {
    public class ViewModel : Notify {
        public Calculator Calculator { get; set; } = new Calculator();
        public string[] Buttons { get; set; } = {
                "MC", "M+", "M-", "MR",
                "C", "+/-", "%", "/",
                "7", "8", "9", "x",
                "4", "5", "6", "-",
                "1", "2", "3", "+",
                "CE", "0", ",", "=",
        };

        private string _result = "0";
        public string Result { get { return _result; } set { _result = value; OnPropertyChanged(); } }
        private string _op = "+";
        public string Op { get { return _op; } set { _op = value; OnPropertyChanged(); } }
        bool userInput = false;
        bool equal = false;
        bool isOp = false;

        public void OnClick(object sender, EventArgs e) {
            string button = (sender as Button).Content.ToString();

            switch(button) {
                case var i when i.All(char.IsDigit):
                    if (equal) { Result = "0"; equal = false; }
                    if (Result.Equals("0")) Result = button;
                    else Result += button;
                    userInput = true;
                    break;
                case ",":
                    if (!Result.Contains(",")) Result += ","; break;
                case "+": 
                case "-": 
                case "x": 
                case "/":
                    if (!isOp) {
                        if (!equal) {
                            Result = Calculator.Calculate(Calculator.Result, Calculator.Op, UserInput()).ToString();
                            equal = true;
                        }
                        userInput = false;
                    }
                    Calculator.Op = button.ToOperator();
                    Op = Calculator.Op.ToSymbol();
                    isOp = true;
                    break;
                case "=":
                    Result = Calculator.Calculate(Calculator.Result, Calculator.Op, UserInput()).ToString();
                    userInput = false;
                    equal = true;
                    break;
                case "CE":
                    Result = Calculator.Result.ToString();
                    break;
                case "C":
                    Calculator.Clear();
                    Result = Calculator.Result.ToString();
                    Op = Calculator.Op.ToSymbol();
                    break;
                case "MC": Calculator.ClearMemory(); break;
                case "M+": Calculator.AddMemory(UserInput()); break;
                case "M-": Calculator.SubtractMemory(UserInput()); break;
                case "MR": Result = Calculator.RecallMemory().ToString(); break;
                case "+/-": 
                    Result = Calculator.Negate().ToString(); 
                    break;
                case "%": 
                    Result = Calculator.Percentage().ToString(); 
                    break;
                    
            }

            isOp = button.IsOperator();
        }

        Fraction UserInput() {
            return userInput ? Fraction.Create(Result) : Calculator.Result;
        }
    }
}
