using System;
using System.Collections.Generic;
using System.Text;

namespace ParentalContributionApp {
    public class ParentalContribution {
        public int MaxAge { get; set; } = 10;
        public int BelowAgeContrib { get; set; } = 2500;
        public int BelowAgeMaxAmount { get; set; } = 3;
        public int OffAgeContrib { get; set; } = 3700;
        public int OffAgeMaxAmount { get; set; } = 2;
        public int MinContrib { get; set; } = 5000;
        public int MaxContrib { get; set; } = 15000;
        public double SingleParentReductionPercentage { get; set; } = 0.25;

        public int GetContribution(ICollection<DateTime> childrenDOB, DateTime date, bool singleParent) {
            if (childrenDOB == null || childrenDOB.Count < 0) return 0;

            int offAge = 0;
            int belowAge = 0;

            foreach(DateTime childDOB in childrenDOB) {
                if (PassedAge(childDOB, date, MaxAge)) {
                    offAge++;
                } else belowAge++;
            }

            int belowAgeContrib = GetContributionAmount(BelowAgeContrib, belowAge, BelowAgeMaxAmount);
            int offAgeContrib = GetContributionAmount(OffAgeContrib, offAge, OffAgeMaxAmount);
            int contribution = Math.Min(MaxContrib, MinContrib + belowAgeContrib + offAgeContrib);
            return singleParent ? (int)(contribution * (1 - SingleParentReductionPercentage)) : contribution;
        }

        public int GetContributionAmount(int contrib, int amount, int maxAmount) {
            return Math.Min((contrib * maxAmount), amount * contrib);
        }

        public bool PassedAge(DateTime dob, DateTime date, int years) {
            return dob.AddYears(years) < date;
        }
    }
}
