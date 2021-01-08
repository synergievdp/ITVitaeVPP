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

namespace EncryptionApp {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        ViewModel viewModel;
        public MainWindow() {
            InitializeComponent();

            viewModel = new ViewModel();
            DataContext = viewModel;
        }

        public void OnClick(object sender, EventArgs e) {
            viewModel.OnClick(sender, e);
        }

        public void OnPasswordChanged(object sender, EventArgs e) {
            viewModel.OnPasswordChanged(sender, e);
        }
    }
}
