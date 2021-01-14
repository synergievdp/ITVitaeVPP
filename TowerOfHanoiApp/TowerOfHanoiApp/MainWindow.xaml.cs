using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

namespace TowerOfHanoiApp {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        public ViewModel ViewModel { get; set; }
        public MainWindow() {
            InitializeComponent();

            ViewModel = new ViewModel();
            DataContext = ViewModel;
        }

        public void OnClick(object sender, EventArgs e) {
            ViewModel.OnClick(sender, e);
        }
    }

    public class ViewModel : Notify {
        public TowerOfHanoi TowerOfHanoi { get; set; }
        public ObservableCollection<Disk> Towers { get; set; }
        public int Columns { get; set; } = 3;
        private int _selectedTower = -1;
        public int SelectedTower {
            get { return _selectedTower; }
            set { _selectedTower = value; OnPropertyChanged(); }
        }

        public ViewModel() {
            TowerOfHanoi = new TowerOfHanoi(3, 8);
            Towers = new ObservableCollection<Disk>();
            Build();
        }

        public void Build() {
            Towers.Clear();
            for(int disk = 8-1; disk > -1; disk--) {
                for(int tower = 0; tower < 3; tower++) {
                    Disk currentDisk = TowerOfHanoi.Towers[tower][disk];
                    Towers.Add(currentDisk==null?Disk.Default:currentDisk);
                }
            }
        }

        public void OnClick(object sender, EventArgs e) {
            MouseEventArgs mouse = e as MouseEventArgs;
            Panel panel = sender as Panel;

            int tower = (int)(mouse.GetPosition(panel).X / (panel.ActualWidth / 3));
            if (SelectedTower == -1) {
                SelectedTower = tower;
            } else {
                if(TowerOfHanoi.Move(SelectedTower, tower)) {
                    SelectedTower = -1;
                    Build();
                } else {
                    SelectedTower = tower;
                }
            }
        }
    }

    public class Notify : INotifyPropertyChanged {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string name = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }

    public class TowerOfHanoi {
        public Disk[][] Towers { get; set; }

        public TowerOfHanoi(int towers, int disks) {
            Towers = new Disk[towers][];

            for(int i = 0; i < towers; i++) {
                Towers[i] = new Disk[disks];
            }

            for (int i = 0; i < disks; i++) {
                Towers[0][i] = new Disk(disks - i);
            }
        }

        public bool Move(int fromTower, int toTower) {
            if (CheckMove(fromTower, toTower)) {
                int fromDisk = FirstEmptyIndex(fromTower) == -1 ? Towers[fromTower].Length - 1 : FirstEmptyIndex(fromTower) - 1;
                Disk disk = Towers[fromTower][fromDisk];
                Towers[toTower][FirstEmptyIndex(toTower)] = disk;
                Towers[fromTower][fromDisk] = null;
                return true;
            } else return false;
        }

        public bool CheckMove(int fromTower, int toTower) {
            if (Towers[fromTower][0] != null && Towers[toTower][0] == null) {
                return true;
            } else {
                return Towers[fromTower][FirstEmptyIndex(fromTower) - 1].Size < Towers[toTower][FirstEmptyIndex(toTower) - 1].Size;
            }
        }

        public int FirstEmptyIndex(int tower) {
            for(int i = 0; i < Towers[tower].Length; i++) {
                if (Towers[tower][i] == null) return i;
            }
            return -1;
        }
    }

    public class Disk {
        public static Disk Default { get; set; } = new Disk(0);
        public int Size { get; private set; }
        public string Color { get; set; }
        public string[] Colors = { "Black", "Yellow", "Green", "Cyan", "Blue", "Violet", "Purple", "Red", "Orange"};

        public Disk(int size) {
            Size = size * 25 + 25;
            Color = Colors[size % Colors.Length];
        }
    }
}
