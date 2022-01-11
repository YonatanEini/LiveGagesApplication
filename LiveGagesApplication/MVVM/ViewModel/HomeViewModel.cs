using LiveGagesApplication.Cors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveGagesApplication.MVVM.ViewModel
{
    /// <summary>
    /// presents the current view
    /// </summary>
    public class MainViewModel : ObservableObject
    {
        public RelayCommand SpeedHeightViewCommand { get; set; }
        public RelayCommand LightsViewCommand { get; set; }

        public SpeedHeightViewModel HomwVm { get; set; }
        public LightsDataView LightsVm { get; set; }
        public static object _currentView;

        public object CurrentView
        {
            get { return _currentView; }
            set
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }

        public MainViewModel()
        {
            HomwVm = new SpeedHeightViewModel();
            LightsVm = new LightsDataView();
            CurrentView = HomwVm;

            SpeedHeightViewCommand = new RelayCommand(o =>
            {
                CurrentView = HomwVm;
            });

            LightsViewCommand = new RelayCommand(o =>
            {
                CurrentView = LightsVm;
            });
        }
    }
}
