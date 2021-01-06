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

namespace FlagsOfTheWorldApp {
    /// <summary>
    /// Interaction logic for QuizView.xaml
    /// </summary>
    public partial class QuizView : UserControl {
        ViewModel viewModel;
        public QuizView(ViewModel viewModel) {
            InitializeComponent();

            this.viewModel = viewModel;
            DataContext = viewModel;
        }

        public void OnRadioButton(object sender, EventArgs e) {
            viewModel.OnRadioButton(sender, e);
        }
    }
}
