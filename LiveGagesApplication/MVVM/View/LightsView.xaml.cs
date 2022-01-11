using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace LiveGagesApplication.MVVM.View
{
    /// <summary>
    /// Interaction logic for LightsView.xaml
    /// </summary>
    public partial class LightsView : UserControl ,INotifyPropertyChanged
    {
        private string _currentReadyLightValue;
        private string _currentCoPilotLightValue;
        private string _currentEngLightValue;
        public string CurrentEngLightValue
        {
            get { return _currentEngLightValue; }
            set
            {
                _currentEngLightValue = value;
                OnPropertyChanged("CurrentEngLightValue");
            }
        }
        public string CurrentCoPillotLightValue
        {
            get { return _currentCoPilotLightValue; }
            set
            {
                _currentCoPilotLightValue = value;
                OnPropertyChanged("CurrentCoPillotLightValue");
            }
        }
        public string CurrentReadyLightValue
        {
            get { return _currentReadyLightValue; }
            set
            {
                _currentReadyLightValue = value;
                OnPropertyChanged("CurrentReadyLightValue");
            }
        }
        public LightsView()
        {
            InitializeComponent();
            this.DataContext = this;
            _instance = this;
            UpdateViewValuesModel.LightWindow = this;
        }
        //to enable property change from outside
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
        private static LightsView _instance;
        //singleton
        public static LightsView GetInstance()
        {
            if (_instance == null)
            {
                _instance = new LightsView();

            }
            return _instance;
        }
    }
}
