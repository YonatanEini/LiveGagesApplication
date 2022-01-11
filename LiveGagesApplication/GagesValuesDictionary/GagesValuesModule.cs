using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveGagesApplication.GagesValuesDictionary
{
    /// <summary>
    /// containes Gages Propeties Names And Current Value
    /// </summary>
    public class GagesValuesModule
    {
        public Dictionary<string, int> GagesValuesDict { get; set; }
        public int TotalFramesAnalyzed { get; set; }
        public GagesValuesModule()
        {
            //initialize dictionary, replace to enum
            this.GagesValuesDict = new Dictionary<string, int>
            {
                { "Height", 0 },
                { "Velocity", 0 },
                { "Wind Speed", 0 },
                { "Light - Ready", 0},
                {"Light - Copilot", 0 },
                { "Light - Eng Cut", 0}
            };
            this.TotalFramesAnalyzed = 0;
        }
        private static GagesValuesModule _instance;
        //singleton
        public static GagesValuesModule GetInstance()
        {
            if (_instance == null)
            {
                _instance = new GagesValuesModule();

            }
            return _instance;
        }
    }
}
