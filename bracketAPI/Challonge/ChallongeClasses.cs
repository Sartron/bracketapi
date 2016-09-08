using RestSharp.Deserializers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.XPath;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace bracketAPI.Challonge
{
    public class ChallongeTournament
    {
        private JObject rawJSON;
        private XElement rawXML;

        public DateTime Completed { get; }
        public DateTime Created { get; }
        public DateTime Started { get; }
        public DateTime Updated { get; }
        public List<ChallongeMatch> Matches { get; }
        public List<ChallongeParticipant> Participants { get; }
        public TournamentState State { get; }
        public TournamentType Bracket { get; }
        public Uri FullURL { get; }
        public Uri ImageURL { get; }
        public int GameID { get; }
        public int ID { get; }
        public int Progress { get; }
        public string Description { get; }
        public string GameName { get; }
        public string Name { get; }
        public string ShortURL { get; }
        public string Subdomain { get; }

        public ChallongeTournament(JObject jObject)
        {
            //Store raw JSON
            rawJSON = jObject;

            //DateTime Properties
            if (!string.IsNullOrWhiteSpace((string)jObject["tournament"]["completed_at"])) Completed = DateTime.Parse((string)jObject["tournament"]["completed_at"]);
            Created = DateTime.Parse((string)jObject["tournament"]["created_at"]);
            if (!string.IsNullOrWhiteSpace((string)jObject["tournament"]["started_at"])) Started = DateTime.Parse((string)jObject["tournament"]["started_at"]);
            Updated = DateTime.Parse((string)jObject["tournament"]["updated_at"]);

            //Class Properties
            if (jObject["tournament"]["matches"] != null)
            {
                //Parse matches
                Debug.WriteLine("Match array found");
            }
            if (jObject["tournament"]["participants"] != null)
            {
                //Parse participants
                Debug.WriteLine("Tournament array found");
            }

            //Enum Properties
            State = ChallongeHelpers.ParseTournamentState((string)jObject["tournament"]["state"]);
            Bracket = ChallongeHelpers.ParseTournamentType((string)jObject["tournament"]["tournament_type"]);

            //Uri Properties
            FullURL = (Uri)jObject["tournament"]["full_challonge_url"];
            ImageURL = (Uri)jObject["tournament"]["live_image_url"];

            //int Properties
            if (!string.IsNullOrWhiteSpace((string)jObject["tournament"]["game_id"])) GameID = (int)jObject["tournament"]["game_id"];
            ID = (int)jObject["tournament"]["id"];
            Progress = (int)jObject["tournament"]["progress_meter"];

            //string Properties
            Description = (string)jObject["tournament"]["description"];
            if (!string.IsNullOrWhiteSpace((string)jObject["tournament"]["game_name"])) GameName = (string)jObject["tournament"]["game_name"];
            Name = (string)jObject["tournament"]["name"];
            ShortURL = (string)jObject["tournament"]["url"];
            if (!string.IsNullOrWhiteSpace((string)jObject["tournament"]["subdomain"])) Subdomain = (string)jObject["tournament"]["subdomain"];
        }

        public ChallongeTournament(XElement xElement)
        {
            //Store raw XML
            rawXML = xElement;

            //DateTime Properties
            if (!string.IsNullOrWhiteSpace(xElement.Element("completed-at").Value)) Completed = DateTime.Parse(xElement.Element("completed-at").Value);
            Created = DateTime.Parse(xElement.Element("created-at").Value);
            if (!string.IsNullOrWhiteSpace(xElement.Element("started-at").Value)) Completed = DateTime.Parse(xElement.Element("started-at").Value);
            Updated = DateTime.Parse(xElement.Element("updated-at").Value);

            //Class Properties
            //

            //Enum Properties
            State = ChallongeHelpers.ParseTournamentState(xElement.Element("state").Value);
            Bracket = ChallongeHelpers.ParseTournamentType(xElement.Element("tournament-type").Value);

            //Uri Properties
            FullURL = new Uri(xElement.Element("full-challonge-url").Value);
            ImageURL = new Uri(xElement.Element("live-image-url").Value);

            //int Properties
            if (!string.IsNullOrWhiteSpace(xElement.Element("game-id").Value)) GameID = Convert.ToInt32(xElement.Element("game-id").Value);
            ID = Convert.ToInt32(xElement.Element("id").Value);
            Progress = Convert.ToInt32(xElement.Element("progress-meter").Value);

            //string Properties
            Description = xElement.Element("description").Value;
            if (!string.IsNullOrWhiteSpace(xElement.Element("game-name").Value)) GameName = xElement.Element("game-name").Value;
            Name = xElement.Element("name").Value;
            ShortURL = xElement.Element("url").Value;
            if (!string.IsNullOrWhiteSpace(xElement.Element("subdomain").Value)) Subdomain = xElement.Element("subdomain").Value;
        }
    }

    public class ChallongeParticipant
    {
        public ChallongeParticipant(JObject jObject)
        {

        }

        public ChallongeParticipant(XElement xElement)
        {

        }
    }

    public class ChallongeMatch
    {
        public ChallongeMatch(JObject jObject)
        {

        }

        public ChallongeMatch(XElement xElement)
        {

        }
    }
}