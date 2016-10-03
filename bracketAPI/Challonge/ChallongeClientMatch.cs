using bracketAPI.Challonge.Enums.Filters;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Net;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace bracketAPI.Challonge
{
    public partial class ChallongeClient
    {
        /// <summary>
        /// Retrieve match from specified tournament ID
        /// </summary>
        public async Task<Match> GetMatch(int tournamentId, int matchId, bool includeAttachments = false)
        {
            //http://api.challonge.com/v1/documents/matches/show

            //Create RestClient & RestRequest
            RestClient restClient = new RestClient("https://api.challonge.com/v1/");
            RestRequest request = new RestRequest($"tournaments/{tournamentId}/matches/{matchId}.json", Method.GET)
            {
                Credentials = new NetworkCredential(Username, APIKey),
                Parameters =
                {
                    new Parameter()
                    {
                        Name = "include_attachments",
                        Value = Convert.ToSingle(includeAttachments),
                        ContentType = null,
                        Type = ParameterType.GetOrPost
                    }
                },
                RequestFormat = DataFormat.Json
            };

            //Pass request
            IRestResponse response = await restClient.ExecuteTaskAsync(request);
            if (response.StatusCode == HttpStatusCode.OK)
                return Helpers.IsNullOrEmpty(JToken.Parse(response.Content)["errors"]) ? JToken.Parse(response.Content)["match"].ToObject<Match>() : null;

            //Request failed
            return null;
        }

        /// <summary>
        /// Retrieve match from specified tournament URL
        /// </summary>
        public async Task<Match> GetMatch(string tournamentUrl, int matchId, string subDomain = null, bool includeAttachments = false)
        {
            //http://api.challonge.com/v1/documents/matches/show

            //Create RestClient & RestRequest
            RestClient restClient = new RestClient("https://api.challonge.com/v1/");
            RestRequest request = new RestRequest($"tournaments/{(!string.IsNullOrWhiteSpace(subDomain) ? $"{subDomain}-" : string.Empty)}{tournamentUrl}/matches/{matchId}.json", Method.GET)
            {
                Credentials = new NetworkCredential(Username, APIKey),
                Parameters =
                {
                    new Parameter()
                    {
                        Name = "include_attachments",
                        Value = Convert.ToSingle(includeAttachments),
                        ContentType = null,
                        Type = ParameterType.GetOrPost
                    }
                },
                RequestFormat = DataFormat.Json
            };

            //Pass request
            IRestResponse response = await restClient.ExecuteTaskAsync(request);
            if (response.StatusCode == HttpStatusCode.OK)
                return Helpers.IsNullOrEmpty(JToken.Parse(response.Content)["errors"]) ? JToken.Parse(response.Content)["match"].ToObject<Match>() : null;

            //Request failed
            return null;
        }

        /// <summary>
        /// Retrieve list of matches from specified tournament ID
        /// </summary>
        public async Task<List<Match>> GetMatches(int tournamentId, MatchState state = MatchState.All, int participantId = -1)
        {
            //http://api.challonge.com/v1/documents/matches/index

            //Create RestClient & RestRequest
            RestClient restClient = new RestClient("https://api.challonge.com/v1/");
            RestRequest request = new RestRequest($"tournaments/{tournamentId}/matches.json", Method.GET)
            {
                Credentials = new NetworkCredential(Username, APIKey),
                Parameters =
                {
                    new Parameter()
                    {
                        Name = "state",
                        Value = Converters.MatchStateConverter(state),
                        ContentType = null,
                        Type = ParameterType.GetOrPost
                    }
                },
                RequestFormat = DataFormat.Json
            };
            if (participantId >= 0)
                request.AddParameter("participant_id", participantId, null, ParameterType.GetOrPost);

            //Pass request
            IRestResponse response = await restClient.ExecuteTaskAsync(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                //Challonge API passed an error [Status Code 422]
                JToken content = JToken.Parse(response.Content);
                if (content.Type != JTokenType.Array && !Helpers.IsNullOrEmpty(content["errors"]))
                    return null;

                List<Match> returnMatches = new List<Match>();
                foreach (JToken match in content)
                    returnMatches.Add(match.First.First.ToObject<Match>());

                return returnMatches;
            }

            //Request failed
            return null;
        }


        /// <summary>
        /// Retrieve list of matches from specified tournament URL
        /// </summary>
        public async Task<List<Match>> GetMatches(string tournamentUrl, string subDomain = null, MatchState state = MatchState.All, int participantId = -1)
        {
            //http://api.challonge.com/v1/documents/matches/index

            //Create RestClient & RestRequest
            RestClient restClient = new RestClient("https://api.challonge.com/v1/");
            RestRequest request = new RestRequest($"tournaments/{(!string.IsNullOrWhiteSpace(subDomain) ? $"{subDomain}-" : string.Empty)}{tournamentUrl}/matches.json", Method.GET)
            {
                Credentials = new NetworkCredential(Username, APIKey),
                Parameters =
                {
                    new Parameter()
                    {
                        Name = "state",
                        Value = Converters.MatchStateConverter(state),
                        ContentType = null,
                        Type = ParameterType.GetOrPost
                    }
                },
                RequestFormat = DataFormat.Json
            };
            if (participantId >= 0)
                request.AddParameter("participant_id", participantId, null, ParameterType.GetOrPost);

            //Pass request
            IRestResponse response = await restClient.ExecuteTaskAsync(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                //Challonge API passed an error [Status Code 422]
                JToken content = JToken.Parse(response.Content);
                if (content.Type != JTokenType.Array && !Helpers.IsNullOrEmpty(content["errors"]))
                    return null;

                List<Match> returnMatches = new List<Match>();
                foreach (JToken match in content)
                    returnMatches.Add(match.First.First.ToObject<Match>());

                return returnMatches;
            }

            //Request failed
            return null;
        }
    }
}