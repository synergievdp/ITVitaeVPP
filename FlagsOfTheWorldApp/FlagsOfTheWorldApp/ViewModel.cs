using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Controls;

namespace FlagsOfTheWorldApp {
    public class ViewModel : Notify {

        public ObservableCollection<Flag> Flags { get; set; }
        public Quiz Quiz { get; set; }
        private Control _currentView;
        public Control CurrentView { get { return _currentView; } set { _currentView = value; OnPropertyChanged(); } }
        public Control flagView;
        public Control quizView;
        private string _quizmsg;
        public string QuizMessage { get { return _quizmsg; } set { _quizmsg = value; OnPropertyChanged(); } }
        private string _quizscore;
        public string QuizScore { get { return _quizscore; } set { _quizscore = value; OnPropertyChanged(); } }

        public ViewModel() {
            Flag.CreateFlags();
            Flags = new ObservableCollection<Flag>(Flag.flags);
            Quiz = new Quiz(Flags);

            flagView = new FlagView(this);
            quizView = new QuizView(this);

            CurrentView = flagView;
        }

        public void OnClick(object sender, EventArgs e) {
            CurrentView = (sender as Button).Content.ToString() switch {
            "FlagView" => flagView,
            "QuizView" => QuizView(),
            };
        }

        public void OnRadioButton(object sender, EventArgs e) {
            string button = (sender as RadioButton).Content.ToString();
            if (Quiz.Check(button)) {
                QuizMessage = String.Format("Correct, it was {0}.", Quiz.Question.CountryName);
            } else QuizMessage = String.Format("Wrong, it was {0}.", Quiz.Question.CountryName);
            QuizScore = String.Format("{0}/{1}", Quiz.CorrectAnswers, Quiz.Flags.Count);
            Quiz.Next();
        }

        Control QuizView() {
            Quiz.Reset();
            QuizScore = String.Format("{0}/{1}", Quiz.CorrectAnswers, Quiz.Flags.Count);
            QuizMessage = "Please select an answer.";
            return quizView;
        }
    }
}
