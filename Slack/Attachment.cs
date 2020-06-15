using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneXus.Server.ExternalTool.Slack
{
    public class Attachment
    {

        [JsonProperty("fallback")]
        public string Fallback { get; set; }

        [JsonProperty("pretext")]
        public string Pretext { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("title_link")]
        public string Title_link { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("color")]
        public string Color { get; set; }

        [JsonProperty("fields")]
        public List<Field> Fields { get; set; }

        [JsonProperty("footer")]
        public string Footer { get; set; }


        [JsonProperty("author_name")]
        public string AuthorName { get; set; }

        public Attachment(string aFallback, string aPretext, string aTitle, string aTitle_link,
                          string aText, string aColor, List<Field> aFields, string aFooter, string aAuthorName)
        {
            this.Fallback = aFallback;
            this.Pretext = aPretext;
            this.Title = aTitle;
            this.Title_link = aTitle_link;
            this.Text = aText;
            this.Color = aColor;
            this.Fields = aFields;
            this.Footer = aFooter;
            this.AuthorName = aAuthorName;
        }
        
    }
}
