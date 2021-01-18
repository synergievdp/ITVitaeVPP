using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace CabFareApp {
    class ViewModel : Notify {
        public TimePickerModel StartTimePicker { get; set; }
        public TimePickerModel EndTimePicker { get; set; }
        public CabFare CabFare { get; set; }

        public DateTime StartDate { get; set; }
        public TimeSpan StartTime { get { return StartTimePicker.Time; } set { StartTimePicker.Time = value; } }
        public DateTime EndDate { get; set; }
        public TimeSpan EndTime { get { return EndTimePicker.Time; } set { EndTimePicker.Time = value; } }
        public int Kilometers { get; set; }

        private string _fare;
        public string Fare { get { return _fare; } set { _fare = value; OnPropertyChanged(); } }

        public ICommand FareCommand { get; private set; }

        public ViewModel() {
            StartTimePicker = new TimePickerModel();
            EndTimePicker = new TimePickerModel();
            CabFare = new CabFare();

            FareCommand = new RelayCommand(param => GetFare());
        }

        public void GetFare() {
            Fare = String.Format("{0:C2}", CabFare.GetFare(Kilometers, StartDate.Add(StartTime), EndDate.Add(EndTime)));
        }
    }
}
