﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CalculatorApp {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        ViewModel ViewModel { get; set; }
        public MainWindow() {
            InitializeComponent();

            ViewModel = new ViewModel();
            DataContext = ViewModel;
        }

        void OnClick(object sender, EventArgs e) {
            ViewModel.OnClick(sender, e);
        }
    }
}
