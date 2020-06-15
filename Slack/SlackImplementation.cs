using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using GeneXus.Server.ExternalTool;

namespace GeneXus.Server.ExternalTool.Slack
{
    public class SlackImplementation : IExternalTool
    {
        private Configuration _configuration;

        public SlackImplementation(string jsonData)
        {
            _configuration = JsonConvert.DeserializeObject<Configuration>(jsonData);
        }

        public List<Result> CommitEvent(CommitData issue)
        {
            List<Result> listResults = new List<Result>();
            SlackClient slack = new SlackClient(this._configuration.Url);

            string user = issue.User;
            string commitDate = issue.CommitDate;
            string comment = issue.Comment;
            string idCommit = issue.CommitId;
            List<string> objects = issue.Objects;


            string aSlackUser = this._configuration.Username;
            //string aSlackChannel = this.configuration.Channel;
            //string aPreText = "A new commit has been made by " + user +":";
            string textUser = "*" + user + "*\n\n";
            string aText = comment + "\n\n";
            if (user != null && user != "")
            {
                aText = textUser + aText;
            }
            string aTitle = "Commit #" + idCommit;
            string aColor = "good";
            string gxServer = this._configuration.GxServer;
            string kb = issue.KBName;
            string aLink = gxServer+kb;
            string aFooter = commitDate;
            if(objects != null) {
                //slack.AddField(null, String.Join(", ", objects.ToArray()), false);
                aText += String.Join(", ", objects.ToArray());
            }
            foreach (string channel in this._configuration.Channel)
            {
                Result result = slack.PostMessage(
                                username: aSlackUser,
                                channel: channel,
                                text: aText,
                                //preText: aPreText, 
                                title: aTitle,
                                title_link: aLink,
                                color: aColor,
                                footer: aFooter
                                //authorName: user
                            );

                listResults.Add(result);
            }
            return listResults;
        }
    }
}
