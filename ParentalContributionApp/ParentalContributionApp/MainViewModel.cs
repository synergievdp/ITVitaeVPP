using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;

namespace ParentalContributionApp {
    class MainViewModel : NotifyPropertyChanged {
        private ParentalContribution parentalContribution = new ParentalContribution();
        public ObservableCollection<DateTime> ChildrenDOB { get; set; } = new ObservableCollection<DateTime>();
        public bool IsSingleParent { get; set; }
        public DateTime ReferenceDate { get; set; } = DateTime.Now;
        private DateTime _childDOB = new DateTime(DateTime.Now.Year, 1, 1);
        public DateTime ChildDOB { get { return _childDOB; } set { _childDOB = value; OnPropertyChanged(); } }
        private string _contribution;
        public string Contribution { get { return _contribution; } set { _contribution = value; OnPropertyChanged(); } }

        public ICommand CalculateCommand { get; private set; }
        public ICommand RemoveCommand { get; private set; }
        public ICommand AddCommand { get; private set; }

        public MainViewModel() {
            CalculateCommand = new RelayCommand(param => Calculate());
            RemoveCommand = new RelayCommand(param => RemoveChild((DateTime)param));
            AddCommand = new RelayCommand(param => AddChild(ChildDOB));
        }

        void AddChild(DateTime childDOB) {
            ChildrenDOB.Add(childDOB);
        }

        void RemoveChild(DateTime childDOB) {
            ChildrenDOB.Remove(childDOB);

            System.Diagnostics.Debug.WriteLine(childDOB);
        }

        void Calculate() {
            Contribution = String.Format("{0:C2}", parentalContribution.GetContribution(ChildrenDOB, ReferenceDate, IsSingleParent)/100);
        }
    }
}
