using LiveGagesApplication.GagesValuesDictionary;
using LiveGagesApplication.GagesValuesDictionary.LightsValuesEnum;
using LiveGagesApplication.MVVM.View;
using LiveGagesApplication.MVVM.ViewModel;
using LiveGagesApplication.UdpServer;
using System;
using System.Collections.Generic;
using System.Linq;
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

namespace LiveGagesApplication
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            _instance = this;
            //activating tcp-server on port 11000
            UdpProtocolServer udpSever = new UdpProtocolServer(11000);
            Task.Factory.StartNew(() => udpSever.ActivateServerAsync());
        }
        private static MainWindow _instance;
        //singleton
        public static async Task<MainWindow> GetInstance()
        {
            //awating until the SpeedHeightView Launch
            await Task.Delay(200);
            if (_instance == null)
            {
                _instance = new MainWindow();

            }
            return _instance;
        }
    }
}
