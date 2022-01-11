using LiveGagesApplication.GagesValuesDictionary;
using LiveGagesApplication.GagesValuesDictionary.LightsValuesEnum;
using LiveGagesApplication.MVVM.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveGagesApplication
{
    public class UpdateViewValuesModel
    {
        public int LiveHeightValue { get; set; }
        public int LiveVelocityValue { get; set; }
        public int LiveWindSpeedValue { get; set; }
        public SpeedHeightWindow GagesWindowContainer { get; set; }
        public static LightsView LightWindow { get; set; }
        public UpdateViewValuesModel()
        {
            this.GagesWindowContainer = SpeedHeightWindow.GetInstance();
            LightWindow = null;
            this.LiveHeightValue = 0;
            this.LiveVelocityValue = 0;
            this.LiveWindSpeedValue = 0;
            Task.Factory.StartNew(() => UpdateLiveHeightValuesAsync());
            Task.Factory.StartNew(() => UpdateLiveVelocityAsync());
            Task.Factory.StartNew(() => UpdateLiveWindSpeedAsync());
        }
        private static UpdateViewValuesModel _instance;
        //singleton
        public static UpdateViewValuesModel GetInstance()
        {
            //awating until the SpeedHeightView Launch
            if (_instance == null)
            {
                _instance = new UpdateViewValuesModel();

            }
            return _instance;
        }
        public void UpdateValues(GagesValuesModule gagesDict)
        {
            //Getting Updated Window Instance
            this.GagesWindowContainer = SpeedHeightWindow.GetInstance();
            //Live Data Values
            this.LiveHeightValue = gagesDict.GagesValuesDict["Height"];
            this.LiveVelocityValue = gagesDict.GagesValuesDict["Velocity"];
            this.LiveWindSpeedValue = gagesDict.GagesValuesDict["Wind Speed"];
            // Calculating AVG
            GagesWindowContainer.CurrentHeightAvg = string.Format("{0:0.00}", CalculateAvg(GagesWindowContainer.CurrentHeightAvg,
                                       LiveHeightValue, gagesDict));
            GagesWindowContainer.CurrentVelocityAvg = string.Format("{0:0.00}", CalculateAvg(GagesWindowContainer.CurrentVelocityAvg,
                                       LiveVelocityValue, gagesDict));
            GagesWindowContainer.CurrentWindVelocityAvg = string.Format("{0:0.00}", CalculateAvg(GagesWindowContainer.CurrentWindVelocityAvg,
                                       LiveWindSpeedValue, gagesDict));
            //updating lights values
            UpdateLightsValues(gagesDict);
            gagesDict.TotalFramesAnalyzed++;
        }
        public async Task UpdateLiveHeightValuesAsync()
        {
            while (true)
            {
                if (GagesWindowContainer.LiveHeight < LiveHeightValue)
                {
                    GagesWindowContainer.LiveHeight++;
                }
                if (GagesWindowContainer.LiveHeight > LiveHeightValue)
                {
                    GagesWindowContainer.LiveHeight--;
                }
                GagesWindowContainer.CurrentReportedHeight = GagesWindowContainer.LiveHeight.ToString();
                await Task.Delay(1);
            
            }
        }
        public async Task UpdateLiveVelocityAsync()
        {
            while (true)
            {
                if (Convert.ToInt32(GagesWindowContainer.CurrentVelocity) < LiveVelocityValue)
                {
                    int newVelocityData = Convert.ToInt32(GagesWindowContainer.CurrentVelocity) + 1;
                    GagesWindowContainer.CurrentVelocity = newVelocityData.ToString();
                }
                if (Convert.ToInt32(GagesWindowContainer.CurrentVelocity) > LiveVelocityValue)
                {
                    int newVelocityData = Convert.ToInt32(GagesWindowContainer.CurrentVelocity) - 1;
                    GagesWindowContainer.CurrentVelocity = newVelocityData.ToString();
                }
                await Task.Delay(1);
            }
        }
        public async Task UpdateLiveWindSpeedAsync()
        {
            while (true)
            {
                if (Convert.ToInt32(GagesWindowContainer.CurrentWindSpeed) < LiveWindSpeedValue)
                {
                    int newVelocityData = Convert.ToInt32(GagesWindowContainer.CurrentWindSpeed) + 1;
                    GagesWindowContainer.CurrentWindSpeed = newVelocityData.ToString();
                }
                if (Convert.ToInt32(GagesWindowContainer.CurrentWindSpeed) > LiveWindSpeedValue)
                {
                    int newVelocityData = Convert.ToInt32(GagesWindowContainer.CurrentWindSpeed) - 1;
                    GagesWindowContainer.CurrentWindSpeed = newVelocityData.ToString();
                }
                await Task.Delay(1);
            }
        }
        public double CalculateAvg(string currentAvg, int newValue, GagesValuesModule gagesModule)
        {
            _ = double.TryParse(currentAvg, out double currentHeightAvg);
            double currentTotalValue = newValue + (currentHeightAvg * gagesModule.TotalFramesAnalyzed);
            int totalFrames = gagesModule.TotalFramesAnalyzed + 1;
            return currentTotalValue / totalFrames;
        }
        public void UpdateLightsValues(GagesValuesModule gagesDict)
        {
            if (LightWindow != null)
            {
                LightWindow.CurrentCoPillotLightValue = ((LightsStatusEnum)gagesDict.GagesValuesDict["Light - Copilot"]).ToString();
                LightWindow.CurrentEngLightValue = ((LightsStatusEnum)gagesDict.GagesValuesDict["Light - Eng Cut"]).ToString();
                LightWindow.CurrentReadyLightValue = ((LightsStatusEnum)gagesDict.GagesValuesDict["Light - Ready"]).ToString();
            }
        }
    }
}
