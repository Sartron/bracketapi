using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Xml.Linq;

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
            if (!Helpers.IsNullOrEmpty(jObject["tournament"]["completed_at"])) Completed = DateTime.Parse((string)jObject["tournament"]["completed_at"]);
            Created = DateTime.Parse((string)jObject["tournament"]["created_at"]);
            if (!Helpers.IsNullOrEmpty(jObject["tournament"]["started_at"])) Started = DateTime.Parse((string)jObject["tournament"]["started_at"]);
            Updated = DateTime.Parse((string)jObject["tournament"]["updated_at"]);

            //Class Properties
            if (!Helpers.IsNullOrEmpty(jObject["tournament"]["matches"]))
            {
                //Create list
                Matches = new List<ChallongeMatch>();

                //Add participants to list
                foreach (JObject match in jObject["tournament"]["matches"])
                    Matches.Add(new ChallongeMatch(match));
            }
            if (!Helpers.IsNullOrEmpty(jObject["tournament"]["participants"]))
            {
                //Create list
                Participants = new List<ChallongeParticipant>();

                //Add participants to list
                foreach (JObject participant in jObject["tournament"]["participants"])
                    Participants.Add(new ChallongeParticipant(participant));
            }

            //Enum Properties
            State = ChallongeHelpers.ParseTournamentState((string)jObject["tournament"]["state"]);
            Bracket = ChallongeHelpers.ParseTournamentType((string)jObject["tournament"]["tournament_type"]);

            //Uri Properties
            FullURL = (Uri)jObject["tournament"]["full_challonge_url"];
            ImageURL = (Uri)jObject["tournament"]["live_image_url"];

            //int Properties
            if (!Helpers.IsNullOrEmpty(jObject["tournament"]["game_id"])) GameID = (int)jObject["tournament"]["game_id"];
            ID = (int)jObject["tournament"]["id"];
            Progress = (int)jObject["tournament"]["progress_meter"];

            //string Properties
            Description = (string)jObject["tournament"]["description"];
            if (!Helpers.IsNullOrEmpty(jObject["tournament"]["game_name"])) GameName = (string)jObject["tournament"]["game_name"];
            Name = (string)jObject["tournament"]["name"];
            ShortURL = (string)jObject["tournament"]["url"];
            if (!Helpers.IsNullOrEmpty(jObject["tournament"]["subdomain"])) Subdomain = (string)jObject["tournament"]["subdomain"];
        }

        public ChallongeTournament(XElement xElement)
        {
            //Store raw XML
            rawXML = xElement;

            //DateTime Properties
            if (!Helpers.IsNullOrEmpty(xElement.Element("completed-at"))) Completed = DateTime.Parse(xElement.Element("completed-at").Value);
            Created = DateTime.Parse(xElement.Element("created-at").Value);
            if (!Helpers.IsNullOrEmpty(xElement.Element("started-at"))) Completed = DateTime.Parse(xElement.Element("started-at").Value);
            Updated = DateTime.Parse(xElement.Element("updated-at").Value);

            //Class Properties
            if (!Helpers.IsNullOrEmpty(xElement.Element("matches")))
            {
                //Create list
                Matches = new List<ChallongeMatch>();

                //Add participants to list
                foreach (XElement match in xElement.Element("matches").Elements())
                    Matches.Add(new ChallongeMatch(match));
            }
            if (!Helpers.IsNullOrEmpty(xElement.Element("participants")))
            {
                //Create list
                Participants = new List<ChallongeParticipant>();

                //Add participants to list
                foreach (XElement participant in xElement.Element("participants").Elements())
                    Participants.Add(new ChallongeParticipant(participant));
            }

            //Enum Properties
            State = ChallongeHelpers.ParseTournamentState(xElement.Element("state").Value);
            Bracket = ChallongeHelpers.ParseTournamentType(xElement.Element("tournament-type").Value);

            //Uri Properties
            FullURL = new Uri(xElement.Element("full-challonge-url").Value);
            ImageURL = new Uri(xElement.Element("live-image-url").Value);

            //int Properties
            if (!Helpers.IsNullOrEmpty(xElement.Element("game-id"))) GameID = Convert.ToInt32(xElement.Element("game-id").Value);
            ID = Convert.ToInt32(xElement.Element("id").Value);
            Progress = Convert.ToInt32(xElement.Element("progress-meter").Value);

            //string Properties
            Description = xElement.Element("description").Value;
            if (!Helpers.IsNullOrEmpty(xElement.Element("game-name"))) GameName = xElement.Element("game-name").Value;
            Name = xElement.Element("name").Value;
            ShortURL = xElement.Element("url").Value;
            if (!Helpers.IsNullOrEmpty(xElement.Element("subdomain"))) Subdomain = xElement.Element("subdomain").Value;
        }
    }

    public class ChallongeParticipant
    {
        private JObject rawJSON;
        private XElement rawXML;

        public List<ChallongeMatch> Matches { get; }
        public int ParticipantID { get; }
        public int Placing { get; }
        public int Seed { get; }
        public int TournamentID { get; }
        public string ChallongeUsername { get; }
        public string PlayerName { get; }

        public ChallongeParticipant(JObject jObject)
        {
            //Store raw JSON
            rawJSON = jObject;

            //Class Properties
            if (!Helpers.IsNullOrEmpty(jObject["participant"]["matches"]))
            {
                //Create list
                Matches = new List<ChallongeMatch>();

                //Add participants to list
                foreach (JObject match in jObject["participant"]["matches"])
                    Matches.Add(new ChallongeMatch(match));
            }

            //int Properties
            ParticipantID = (int)jObject["participant"]["id"];
            if (!Helpers.IsNullOrEmpty(jObject["participant"]["final_rank"])) Placing = (int)jObject["participant"]["final_rank"];
            Seed = (int)jObject["participant"]["seed"];
            TournamentID = (int)jObject["participant"]["tournament_id"];

            //string Properties
            if (!Helpers.IsNullOrEmpty(jObject["participant"]["challonge_username"])) ChallongeUsername = (string)jObject["participant"]["challonge_username"];
            PlayerName = (string)jObject["participant"]["name"];
        }

        public ChallongeParticipant(XElement xElement)
        {
            //Store raw XML
            rawXML = xElement;

            //Class Properties
            if (!Helpers.IsNullOrEmpty(xElement.Element("matches")))
            {
                //Create list
                Matches = new List<ChallongeMatch>();

                //Add participants to list
                foreach (XElement match in xElement.Element("matches").Elements())
                    Matches.Add(new ChallongeMatch(match));
            }

            //int Properties
            ParticipantID = Convert.ToInt32(xElement.Element("id").Value);
            if (!Helpers.IsNullOrEmpty(xElement.Element("final-rank"))) Placing = Convert.ToInt32(xElement.Element("final-rank").Value);
            Seed = Convert.ToInt32(xElement.Element("seed").Value);
            TournamentID = Convert.ToInt32(xElement.Element("tournament-id").Value);

            //string Properties
            if (!Helpers.IsNullOrEmpty(xElement.Element("challonge_username"))) ChallongeUsername = xElement.Element("challonge_username").Value;
            PlayerName = xElement.Element("name").Value;
        }
    }

    public class ChallongeMatch
    {
        private JObject rawJSON;
        private XElement rawXML;

        public int LoserParticipantID { get; }
        public int MatchID { get; }
        public int Participant1_ID { get; }
        public int Participant1_PreReq_MatchID { get; }
        public int Participant2_ID { get; }
        public int Participant2_PreReq_MatchID { get; }
        public int Round { get; }
        public int TournamentID { get; }
        public int WinnerParticipantID { get; }

        public ChallongeMatch(JObject jObject)
        {
            //Store raw JSON
            rawJSON = jObject;

            //Class Properties
            //

            //int Properties
            if (!Helpers.IsNullOrEmpty(jObject["match"]["loser_id"])) LoserParticipantID = (int)jObject["match"]["loser_id"];
            MatchID = (int)jObject["match"]["id"];
            if (!Helpers.IsNullOrEmpty(jObject["match"]["player1_id"])) Participant1_ID = (int)jObject["match"]["player1_id"];
            if (!Helpers.IsNullOrEmpty(jObject["match"]["player1_prereq_match_id"])) Participant1_PreReq_MatchID = (int)jObject["match"]["player1_prereq_match_id"];
            if (!Helpers.IsNullOrEmpty(jObject["match"]["player2_id"])) Participant2_ID = (int)jObject["match"]["player2_id"];
            if (!Helpers.IsNullOrEmpty(jObject["match"]["player2_prereq_match_id"])) Participant2_PreReq_MatchID = (int)jObject["match"]["player2_prereq_match_id"];
            Round = (int)jObject["match"]["round"];
            TournamentID = (int)jObject["match"]["tournament_id"];
            if (!Helpers.IsNullOrEmpty(jObject["match"]["winner_id"])) WinnerParticipantID = (int)jObject["match"]["winner_id"];
        }

        public ChallongeMatch(XElement xElement)
        {
            //Store raw XML
            rawXML = xElement;

            //Class Properties
            //

            //int Properties
            if (!Helpers.IsNullOrEmpty(xElement.Element("loser-id"))) LoserParticipantID = Convert.ToInt32(xElement.Element("loser-id").Value);
            MatchID = Convert.ToInt32(xElement.Element("id").Value);
            if (!Helpers.IsNullOrEmpty(xElement.Element("player1-id"))) Participant1_ID = Convert.ToInt32(xElement.Element("player1-id").Value);
            if (!Helpers.IsNullOrEmpty(xElement.Element("player1-prereq-match-id"))) Participant1_PreReq_MatchID = Convert.ToInt32(xElement.Element("player1-prereq-match-id").Value);
            if (!Helpers.IsNullOrEmpty(xElement.Element("player2-id"))) Participant2_ID = Convert.ToInt32(xElement.Element("player2-id").Value);
            if (!Helpers.IsNullOrEmpty(xElement.Element("player2-prereq-match-id"))) Participant2_PreReq_MatchID = Convert.ToInt32(xElement.Element("player2-prereq-match-id").Value);
            Round = Convert.ToInt32(xElement.Element("round").Value);
            TournamentID = Convert.ToInt32(xElement.Element("tournament-id").Value);
            if (!Helpers.IsNullOrEmpty(xElement.Element("winner_id"))) WinnerParticipantID = Convert.ToInt32(xElement.Element("winner_id").Value);
        }
    }
}