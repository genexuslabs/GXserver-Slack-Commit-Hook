using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneXus.Server.ExternalTool.Slack
{
    public class Payload
    {
        [JsonProperty("channel")]
        public string Channel { get; set; }

        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("attachments")]
        public List<Attachment> Attachments { get; set; }
        public Payload(string aChannel, string aUsername, List<Attachment> aAttachments)
        {
            this.Channel = aChannel;
            this.Username = aUsername;
            this.Attachments = aAttachments;
        }

    }
}
