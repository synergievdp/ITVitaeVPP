using System;
using System.Collections.Generic;
using System.Text;

namespace CabFareApp {
    public class CabFare {
        public int BaseFare { get; private set; } = 100;
        public int DayFare { get; private set; } = 25;
        public int NightFare { get; private set; } = 45;
        public double WeekendTax { get; private set; } = 0.15;
        public TimeSpan StartWork { get; private set; } = new TimeSpan(8, 0, 0);
        public TimeSpan EndWork { get; private set; } = new TimeSpan(18, 0, 0);

        public int GetFare(int kilometers, DateTime startTime, DateTime endTime) {
            int fare = 0;

            fare += kilometers * BaseFare;

            TimeSpan workhours = GetWorkHours(startTime, endTime);
            fare += (int)workhours.TotalMinutes * DayFare;
            fare += (int)((endTime - startTime) - workhours).TotalMinutes * NightFare;

            if (IsWeekend(startTime)) fare = (int)(fare * (1 + WeekendTax));

            return fare;
        }

        public TimeSpan GetWorkHours(DateTime start, DateTime end) {
            TimeSpan workhours = new TimeSpan();
            TimeSpan startTime = start.TimeOfDay;
            DateTime tomorrow = new DateTime(start.Year, start.Month, start.Day + 1);

            while (end >= tomorrow) {
                TimeSpan endTime = new TimeSpan(24, 0, 0);

                workhours += GetWorkHours(startTime, endTime);

                startTime = TimeSpan.Zero;
                tomorrow = tomorrow.AddDays(1);
            }

            if (end <= tomorrow) {
                workhours += GetWorkHours(startTime, end.TimeOfDay);
            }

            return workhours;
        }

        public TimeSpan GetWorkHours(TimeSpan start, TimeSpan end) {
            if (end > StartWork && start < EndWork) {
                if (start < StartWork) start = StartWork;
                if (end > EndWork) end = EndWork;
                return end - start;
            } else return TimeSpan.Zero;
            
        }

        public bool IsWeekend(DateTime startTime) {
            if(startTime.DayOfWeek == DayOfWeek.Saturday || startTime.DayOfWeek == DayOfWeek.Sunday ||
                (startTime.DayOfWeek == DayOfWeek.Friday && startTime.TimeOfDay >= new TimeSpan(22,0,0)) ||
                (startTime.DayOfWeek == DayOfWeek.Monday && startTime.TimeOfDay < new TimeSpan(7,0,0))) {
                return true;
            } else return false;
        }
    }
}
