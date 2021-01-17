using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CabFareApp {
    /// <summary>
    /// Interaction logic for TimePicker.xaml
    /// </summary>
    public partial class TimePicker : UserControl {
        public int[] Hours { get; private set; }
        public int[] Minutes { get; private set; }
        public int Hour { get; set; }
        public int Minute { get; set; }
        public TimeSpan Time { get { return new TimeSpan(Hour, Minute, 0); } set { Hour = (int)value.TotalHours; Minute = (int)value.TotalMinutes; } }

        public TimePicker() {
            InitializeComponent();
            DataContext = this;

            Hours = new int[24];
            for (int i = 0; i < Hours.Length; i++) Hours[i] = i;
            Minutes = new int[60];
            for (int i = 0; i < Minutes.Length; i++) Minutes[i] = i;
        }
    }
}
