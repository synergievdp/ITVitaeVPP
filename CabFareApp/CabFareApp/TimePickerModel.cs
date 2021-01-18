using System;
using System.Collections.Generic;
using System.Text;

namespace CabFareApp {
    public class TimePickerModel {
        public int[] Hours { get; private set; }
        public int[] Minutes { get; private set; }
        public int Hour { get; set; }
        public int Minute { get; set; }
        public TimeSpan Time { get { return new TimeSpan(Hour, Minute, 0); } set { Hour = (int)value.TotalHours; Minute = (int)value.TotalMinutes; } }

        public TimePickerModel() {
            Hours = new int[24];
            for (int i = 0; i < Hours.Length; i++) Hours[i] = i;
            Minutes = new int[60];
            for (int i = 0; i < Minutes.Length; i++) Minutes[i] = i;
        }
    }
}
