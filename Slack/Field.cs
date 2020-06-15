using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneXus.Server.ExternalTool.Slack
{
    public class Field
    {
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }

        [JsonProperty("short")]
        public bool Short { get; set; }
        public Field(string aTitle, string aValue, bool aShort)
        {
            this.Title = aTitle;
            this.Value = aValue;
            this.Short = aShort;
        }

    }
}
