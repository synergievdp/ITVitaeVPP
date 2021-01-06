using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace FlagsOfTheWorldApp {
    public class Quiz : Notify {
        public ObservableCollection<Flag> Flags { get; set; }
        List<Flag> Asked { get; set; }
        public ObservableCollection<Flag> Current { get; set; }
        private Flag _question;
        public Flag Question { get { return _question; } set { _question = value; OnPropertyChanged(); } }
        public int CorrectAnswers { get; set; }

        public Quiz(IEnumerable<Flag> flags) {
            Flags = new ObservableCollection<Flag>(flags);
            Asked = new List<Flag>();
            Current = new ObservableCollection<Flag>();
            Reset();
        }

        public void Next() {
            if (Asked.Count == Flags.Count) {
                Reset();
                return;
            }

            Current.Clear();
            Random r = new Random();

            int index;
            for (int i = 0; i < 5; i++) {
                index = r.Next(0, Flags.Count);
                if (!Current.Contains(Flags[index]))
                    Current.Add(Flags[index]);
            }

            index = r.Next(0, Current.Count);
            while (Asked.Contains(Current[index])) {
                Current[index] = Flags[r.Next(0, Flags.Count)];
            }
            Question = Current[index];

            Asked.Add(Question);
        }

        public bool Check(string answer) {
            if (Question.CountryName.Equals(answer)) { 
                CorrectAnswers++; 
                return true; 
            } else return false;
        }

        public void Reset() {
            Asked.Clear();
            Next();
        }
    }
}
