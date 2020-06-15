using Newtonsoft.Json;
using System.Collections.Generic;

namespace GeneXus.Server.ExternalTool.Slack
{
    public class CommitInfo
    {
        [JsonProperty("user")]
        public string User { get; set; }

        [JsonProperty("commitDate")]
        public string CommitDate { get; set; }

        [JsonProperty("comment")]
        public string Comment { get; set; }

        [JsonProperty("idCommit")]
        public string IdCommit { get; set; }

        [JsonProperty("kbname")]
        public string KbName { get; set; }

        [JsonProperty("listObject")]
        public List<string> ListObject { get; set; }

        public CommitInfo(string aUser, string aCommitDate, string aComment, string aIdCommit, 
            string aKbName, List<string> aListObject)
        {
            this.User = aUser;
            this.CommitDate = aCommitDate;
            this.Comment = aComment;
            this.IdCommit = aIdCommit;
            this.ListObject = aListObject;
            this.KbName = aKbName;
        }

    }
}
