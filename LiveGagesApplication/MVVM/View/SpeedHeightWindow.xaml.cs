using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
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
using System.Windows.Threading;

namespace LiveGagesApplication.MVVM.View
{
    /// <summary>
    /// Interaction logic for SpeedHeightWindow.xaml
    /// </summary>
    public partial class SpeedHeightWindow : UserControl, INotifyPropertyChanged
    {
        //gages current values
        private string _currentVelocity;
        private int _liveValue;
        private string _currentReportedHeight;
        private string _currentHeightAvg;
        private string _currentWindSpeed;
        private string _currentVelocityAvg;
        private string _currentWindVelocityAvg;
        public string CurrentWindVelocityAvg
        {
            get { return _currentWindVelocityAvg; }
            set
            {
                _currentWindVelocityAvg = value;
                OnPropertyChanged("CurrentWindVelocityAvg");
            }
        }
        public string CurrentVelocityAvg
        {
            get { return _currentVelocityAvg; }
            set
            {
                _currentVelocityAvg = value;
                OnPropertyChanged("CurrentVelocityAvg");
            }
        }
        public string CurrentWindSpeed
        {
            get { return _currentWindSpeed; }
            set
            {
                _currentWindSpeed = value;
                OnPropertyChanged("CurrentWindSpeed");
            }
        }
        public string CurrentVelocity
        {
            get { return _currentVelocity; }
            set
            {
                _currentVelocity = value;
                OnPropertyChanged("CurrentVelocity");
            }
        }
        public string CurrentHeightAvg
        {
            get { return _currentHeightAvg; }
            set
            {
                _currentHeightAvg = value;
                OnPropertyChanged("CurrentHeightAvg");
            }
        }
        public string CurrentReportedHeight
        {
            get { return _currentReportedHeight; }
            set
            {
                _currentReportedHeight = value;
                OnPropertyChanged("CurrentReportedHeight");
            }
        }
        public int LiveHeight
        {
            get { return _liveValue; }
            set
            {
                _liveValue = value;
                OnPropertyChanged("LiveHeight");
            }
        }
        public void UpdateValue()
        {
            LiveHeight = 120;
        }
        public SpeedHeightWindow()
        {
            InitializeComponent();
            this.DataContext = this;
            this.CurrentHeight.Maximum = 256;
            CurrentVelocity = "0";
            CurrentWindSpeed = "0";
            CurrentHeightAvg = "0.00";
            CurrentReportedHeight = "0";
            LiveHeight = 0;
            CurrentWindVelocityAvg = "0.00";
            CurrentVelocityAvg = "0.00";
            _instance = this;
        }
        //to enable property change from outside
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
        private static SpeedHeightWindow _instance;
        //singleton
        public static SpeedHeightWindow GetInstance()
        {
            if (_instance == null)
            {
                _instance = new SpeedHeightWindow();

            }
            return _instance;
        }
    }
}
