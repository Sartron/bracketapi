﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharp.Authenticators;
using RestSharp.Deserializers;
using RestSharp.Extensions;

namespace bracketAPI.Challonge
{
    /// <summary>
    /// C# Wrapper used to access Challonge API
    /// </summary>
    public class ChallongeClient
    {
        /// <summary>
        /// Challonge Username
        /// </summary>
        public string Username { get; }

        /// <summary>
        /// Challonge Developer API Key
        /// </summary>
        private string APIKey { get; }

        public ChallongeClient(string username, string apikey)
        {
            Username = username;
            APIKey = apikey;
        }

        private string ParseEnum(TournamentState tournamentState)
        {
            switch (tournamentState)
            {
                case TournamentState.All:
                    return "all";
                case TournamentState.Pending:
                    return "pending";
                case TournamentState.InProgress:
                    return "in_progress";
                case TournamentState.Ended:
                    return "ended";
            }

            //Null
            return string.Empty;
        }

        private string ParseEnum(TournamentType tournamentType)
        {
            switch (tournamentType)
            {
                case TournamentType.SingleElimination:
                    return "single_elimination";
                case TournamentType.DoubleElimination:
                    return "double_elimination";
                case TournamentType.RoundRobin:
                    return "round_robin";
                case TournamentType.Swiss:
                    return "swiss";
            }

            //TournamentType.All || Null
            return string.Empty;
        }

        /// <summary>
        /// Retrieve tournament from specified ID
        /// </summary>
        public async Task<ChallongeTournament> GetTournament(int tournamentId, bool includeParticipants = false, bool includeMatches = false, DataFormat dataFormat = DataFormat.JSON)
        {
            //http://api.challonge.com/v1/documents/tournaments/show

            //Create RestClient & RestRequest
            RestClient restClient = new RestClient("https://api.challonge.com/v1/");
            RestRequest request = new RestRequest($"tournaments/{tournamentId}.{dataFormat.ToString().ToLower()}", Method.GET)
            {
                Credentials = new NetworkCredential(Username, APIKey),
                Parameters =
                {
                    new Parameter() { Name = "include_participants", Value = Convert.ToSingle(includeParticipants) },
                    new Parameter() { Name = "include_matches", Value = Convert.ToSingle(includeMatches) }
                },
                RequestFormat = (RestSharp.DataFormat)dataFormat
            };

            //Pass request
            IRestResponse response = await restClient.ExecuteTaskAsync(request);
            if (response.StatusCode == HttpStatusCode.OK)
                return new ChallongeTournament(response.Content, dataFormat);

            //Failed somewhere along the line
            return null;
        }

        /// <summary>
        /// Retrieve tournament from specified tournament URL
        /// </summary>
        public void GetTournament(string tournamentUrl, bool includeParticipants = false, bool includeMatches = false, DataFormat dataFormat = DataFormat.JSON)
        {
            //http://api.challonge.com/v1/documents/tournaments/show
            //https://api.challonge.com/v1/tournaments/{tournament}.json
            
        }

        /// <summary>
        /// Retrieve list of tournaments falling under specified parameters
        /// </summary>
        public async Task<List<ChallongeTournament>> GetTournaments(TournamentState state = TournamentState.All, TournamentType type = TournamentType.All, DateTime createdAfter = default(DateTime), DateTime createdBefore = default(DateTime), string subDomain = "", DataFormat dataFormat = DataFormat.JSON)
        {
            //http://api.challonge.com/v1/documents/tournaments/index

            //Create RestClient & RestRequest
            RestClient restClient = new RestClient("https://api.challonge.com/v1/");
            RestRequest request = new RestRequest($"tournaments.{dataFormat.ToString().ToLower()}", Method.GET)
            {
                Credentials = new NetworkCredential(Username, APIKey),
                Parameters =
                {
                    new Parameter() { Name = "state", Value = ParseEnum(state) },
                    //!string.IsNullOrWhiteSpace(ParseEnum(type)) ? new Parameter() { Name = "type", Value = ParseEnum(type) } : null,
                    new Parameter() { Name = "created_after", Value = createdAfter.ToString("yyyy-MM-dd") },
                    new Parameter() { Name = "created_before", Value = createdBefore.ToString("yyyy-MM-dd") },
                    //!string.IsNullOrWhiteSpace(subDomain) ? new Parameter() { Name = "subdomain", Value = subDomain } : null
                },
                RequestFormat = (RestSharp.DataFormat)dataFormat
            };

            //Pass request
            IRestResponse response = await restClient.ExecuteTaskAsync(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return new List<ChallongeTournament>();
            }

            return null;
        }

        /// <summary>
        /// Retrieve participant from specified tournament ID
        /// </summary>
        public void GetParticipant(int tournamentId, int participantId, bool includeMatches = false)
        {
            //http://api.challonge.com/v1/documents/tournaments/show
            //https://api.challonge.com/v1/tournaments/{tournament}.json
            
        }

        /// <summary>
        /// Retrieve participant from specified tournament URL
        /// </summary>
        public void GetParticipant(string tournamentUrl, int participantId, bool includeMatches = false)
        {
            //http://api.challonge.com/v1/documents/tournaments/show
            // https://api.challonge.com/v1/tournaments/{tournament}/participants/{participant_id}.json
            
        }

        /// <summary>
        /// Retrieve list of participants from specified tournament ID
        /// </summary>
        public void GetParticipants(int tournamentId)
        {
            //http://api.challonge.com/v1/documents/participants/index
            //https://api.challonge.com/v1/tournaments/{tournament}/participants.json
            
        }

        /// <summary>
        /// Retrieve list of participants from specified tournament URL
        /// </summary>
        public void GetParticipants(string tournamentUrl)
        {
            //http://api.challonge.com/v1/documents/participants/index
            //https://api.challonge.com/v1/tournaments/{tournament}/participants.json
            
        }

        /// <summary>
        /// Retrieve match from specified tournament ID
        /// </summary>
        public void GetMatch(int tournamentId, int matchId, bool includeAttachments = false)
        {
            //http://api.challonge.com/v1/documents/matches/show
            //https://api.challonge.com/v1/tournaments/{tournament}/matches/{match_id}.json
            
        }

        /// <summary>
        /// Retrieve match from specified tournament URL
        /// </summary>
        public void GetMatch(string tournamentUrl, int matchId, bool includeAttachments = false)
        {
            //http://api.challonge.com/v1/documents/matches/show
            //https://api.challonge.com/v1/tournaments/{tournament}/matches/{match_id}.json
            
        }

        /// <summary>
        /// Retrieve list of matches from specified tournament ID
        /// </summary>
        public void GetMatches(int tournamentId, TournamentState state = TournamentState.All, int participantId = 0)
        {
            //http://api.challonge.com/v1/documents/matches/index
            //https://api.challonge.com/v1/tournaments/{tournament}/matches.json
            
        }

        /// <summary>
        /// Retrieve list of matches from specified tournament URL
        /// </summary>
        public void GetMatches(string tournamentUrl, TournamentState state = TournamentState.All, int participantId = 0)
        {
            //http://api.challonge.com/v1/documents/matches/index
            //https://api.challonge.com/v1/tournaments/{tournament}/matches.json

        }

        /// <summary>
        /// Retrieve match attachment from specified tournament ID
        /// </summary>
        public void GetMatchAttachment(int tournamentId, int matchId, int attachmentId)
        {
            //http://api.challonge.com/v1/documents/match_attachments/show
            //https://api.challonge.com/v1/tournaments/{tournament}/matches/{match_id}/attachments/{attachment_id}.json

        }

        /// <summary>
        /// Retrieve match attachment from specified tournament URL
        /// </summary>
        public void GetMatchAttachment(string tournamentUrl, int matchId, int attachmentId)
        {
            //http://api.challonge.com/v1/documents/match_attachments/show
            //https://api.challonge.com/v1/tournaments/{tournament}/matches/{match_id}/attachments/{attachment_id}.json

        }

        /// <summary>
        /// Retrieve list of match attachments from specified tournament ID
        /// </summary>
        public void GetMatchAttachments(int tournamentId, int matchId)
        {
            //http://api.challonge.com/v1/documents/match_attachments/index
            //https://api.challonge.com/v1/tournaments/{tournament}/matches/{match_id}/attachments.json

        }

        /// <summary>
        /// Retrieve list of match attachments from specified tournament URL
        /// </summary>
        public void GetMatchAttachments(string tournamentUrl, int matchId)
        {
            //http://api.challonge.com/v1/documents/match_attachments/index
            //https://api.challonge.com/v1/tournaments/{tournament}/matches/{match_id}/attachments.json

        }
    }
}