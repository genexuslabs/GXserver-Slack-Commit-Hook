using System.Collections.Generic;

namespace GeneXus.Server.ExternalTool
{
    public class IssueElement
    {
        public string Id { get; set; }
        public string Status { get; set; }
        public string Project { get; set; }
        public string To { get; set; }

        public IssueElement(string i)
            : this(i, string.Empty)
        {
        }

        public IssueElement(string i, string s)
            : this(i, s, string.Empty)
        {
        }

        public IssueElement(string i, string s, string p)
           : this(i, s, p, string.Empty)
        {
        }

        public IssueElement(string i, string s, string p, string t)
        {
            Id = i;
            Status = s;
            Project = p;
            To = t;
        }
        public IssueElement()
        {
            
        }
    }

    public class CommitData

    {
        public string User { get; set; }
        public string CommitDate { get; set; }

        public string KBName { get; set; }

        public string CommitId { get; set; }

        public List<string> Objects { get; set; }

        public string Comment { get; set; }

        public CommitData(string username, string issuecommitTimeStamp, string IssueKB, string IssueCommitNbr , List<string> listObjects, string comment)

        {
            User = username;
            CommitDate = issuecommitTimeStamp;
            KBName = IssueKB;
            CommitId = IssueCommitNbr;
            Objects = listObjects;
            Comment = comment;
        }
    }
}
