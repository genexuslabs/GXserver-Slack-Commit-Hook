using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneXus.Server.ExternalTool.Slack
{
    public class Configuration
    {

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("channel")]
        public List<string> Channel { get; set; }

        [JsonProperty("gxServer")]
        public string GxServer { get; set; }
        public Configuration(string aUrl, string aUserName, List<string> aChannels)
        {
            this.Url = aUrl;
            this.Username = aUserName;
            this.Channel = aChannels;
        }

    }
}
