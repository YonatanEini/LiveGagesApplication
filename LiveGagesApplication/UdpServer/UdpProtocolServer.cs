using LiveGagesApplication.DecodedFrameModule;
using LiveGagesApplication.GagesValuesDictionary;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace LiveGagesApplication.UdpServer
{
    public class UdpProtocolServer
    {
        public UdpClient UdpServer { get; set; }
        public int PortNumber { get; set; }
        public MainWindow MainWindowScreen { get; set; }
        public UpdateViewValuesModel UpdateValuesModel { get; set; }
        public UdpProtocolServer(int portNumber)
        {
            this.PortNumber = portNumber;
            this.UdpServer = new UdpClient();
        }
        public async Task ActivateServerAsync()
        {
            try
            {
                this.MainWindowScreen = await MainWindow.GetInstance();
                this.UpdateValuesModel = UpdateViewValuesModel.GetInstance();
                IPEndPoint ipep = new IPEndPoint(IPAddress.Any, this.PortNumber);
                this.UdpServer = new UdpClient(ipep);
                _ = Task.Factory.StartNew(() => AcceptCalls());
                Console.WriteLine($"Activating Udp Server on Port {this.PortNumber}");

            }
            catch (SocketException)
            {
                Console.WriteLine("Port Already In Use");
            }
            catch (ArgumentOutOfRangeException)
            {
                Console.WriteLine("Invalid Parameters");
            }
        }
        public void AcceptCalls()
        {
            IPEndPoint sender = new IPEndPoint(IPAddress.Any, 0);
            while (true)
            {
                byte[] data = this.UdpServer.Receive(ref sender);
                string decodedFrameJsonFormat = Encoding.ASCII.GetString(data, 0, data.Length);
                DecodedFrameDto decodedFrame = JsonConvert.DeserializeObject<DecodedFrameDto>(decodedFrameJsonFormat);
                Task.Factory.StartNew(() => UpdateGagesValue(decodedFrame));
            }
        }
        public void UpdateGagesValue(DecodedFrameDto latestDecodedFrame)
        {
            GagesValuesModule gagesDict = GagesValuesModule.GetInstance();
            foreach (DecodedItem item in latestDecodedFrame.DecodedItems)
            {
                if (gagesDict.GagesValuesDict.ContainsKey(item.Name))
                {
                    gagesDict.GagesValuesDict[item.Name] = item.Value;
                }
            }
            this.UpdateValuesModel.UpdateValues(gagesDict);
            gagesDict.TotalFramesAnalyzed++;
        }

    }
}
