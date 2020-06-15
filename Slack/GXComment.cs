using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementacion_Slack
{
    public class GXComment
    {
        public GXComment(string aUser, string aCommitDate, string aComment, string aIdCommit, 
            List<string> aListObject)
        {
            this.User = aUser;
            this.CommitDate = aCommitDate;
            this.Comment = aComment;
            this.IdCommit = aIdCommit;
            this.ListObject = aListObject;
        }

        [JsonProperty("user")]
        public string User { get; set; }

        [JsonProperty("commitDate")]
        public string CommitDate { get; set; }

        [JsonProperty("comment")]
        public string Comment { get; set; }

        [JsonProperty("idCommit")]
        public string IdCommit { get; set; }

        [JsonProperty("listObject")]
        public List<string> ListObject { get; set; }
    }
}
