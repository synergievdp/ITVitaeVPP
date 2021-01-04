using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace CalculatorApp {
    public class Notify : INotifyPropertyChanged {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string name = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
