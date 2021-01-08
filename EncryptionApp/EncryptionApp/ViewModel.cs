using Microsoft.Win32;
using System;
using System.ComponentModel;
using System.Windows.Controls;

namespace EncryptionApp {
    public class ViewModel : Notify {
        public Encryption Encryption { get; set; } = new Encryption();
        public FileHandler FileHandler { get; set; } = new FileHandler();
        private string _input = "Put text here or open a file.";
        public string Input { get { return _input; } set { _input = value; OnPropertyChanged(); } }
        private string _output;
        public string Output { get { return _output; } set { _output = value; OnPropertyChanged(); } }
        public string Password { get; set; } = "password";

        public void OnPasswordChanged(object sender, EventArgs e) {
            string psword = (sender as PasswordBox).Password;
            Password = (psword.Length == 0)? "password" : psword;
        }

        public void OnClick(object sender, EventArgs e) {
            switch ((sender as Button).Name) {
                case "Encrypt":
                    Output = Encryption.Encrypt(Input, Password);
                    break;
                case "Decrypt":
                    Output = Encryption.Decrypt(Input, Password);
                    break;
                case "Open":
                    OpenFileDialog openFileDialog = new OpenFileDialog();
                    if (openFileDialog.ShowDialog() == true) { 
                        Input = FileHandler.Load(openFileDialog.FileName);
                    }
                    break;
                case "Save":
                    SaveFileDialog saveFileDialog = new SaveFileDialog();
                    if(saveFileDialog.ShowDialog() == true) {
                        FileHandler.Save(saveFileDialog.FileName, Output);
                    }
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
}
