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
        /// Retrieve participant from specified tournament ID
        /// </summary>
        public async Task<Participant> GetParticipant(int tournamentId, int participantId, bool includeMatches = false)
        {
            //http://api.challonge.com/v1/documents/participants/show

            //Create RestClient & RestRequest
            RestClient restClient = new RestClient("https://api.challonge.com/v1/");
            RestRequest request = new RestRequest($"tournaments/{tournamentId}/participants/{participantId}.json", Method.GET)
            {
                Credentials = new NetworkCredential(Username, APIKey),
                Parameters =
                {
                    new Parameter()
                    {
                        Name = "include_matches",
                        Value = Convert.ToSingle(includeMatches),
                        ContentType = null,
                        Type = ParameterType.GetOrPost
                    }
                },
                RequestFormat = DataFormat.Json
            };

            //Pass request
            IRestResponse response = await restClient.ExecuteTaskAsync(request);
            if (response.StatusCode == HttpStatusCode.OK)
                return Helpers.IsNullOrEmpty(JToken.Parse(response.Content)["errors"]) ? JToken.Parse(response.Content)["participant"].ToObject<Participant>() : null;

            //Request failed
            return null;
        }

        /// <summary>
        /// Retrieve participant from specified tournament URL
        /// </summary>
        public async Task<Participant> GetParticipant(string tournamentUrl, int participantId, string subDomain = null, bool includeMatches = false)
        {
            //http://api.challonge.com/v1/documents/participants/show

            //Create RestClient & RestRequest
            RestClient restClient = new RestClient("https://api.challonge.com/v1/");
            RestRequest request = new RestRequest($"tournaments/{(!string.IsNullOrWhiteSpace(subDomain) ? $"{subDomain}-" : string.Empty)}{tournamentUrl}/participants/{participantId}.json", Method.GET)
            {
                Credentials = new NetworkCredential(Username, APIKey),
                Parameters =
                {
                    new Parameter()
                    {
                        Name = "include_matches",
                        Value = Convert.ToSingle(includeMatches),
                        ContentType = null,
                        Type = ParameterType.GetOrPost
                    }
                },
                RequestFormat = DataFormat.Json
            };

            //Pass request
            IRestResponse response = await restClient.ExecuteTaskAsync(request);
            if (response.StatusCode == HttpStatusCode.OK)
                return Helpers.IsNullOrEmpty(JToken.Parse(response.Content)["errors"]) ? JToken.Parse(response.Content)["participant"].ToObject<Participant>() : null;

            //Request failed
            return null;
        }

        /// <summary>
        /// Retrieve list of participants from specified tournament ID
        /// </summary>
        public async Task<List<Participant>> GetParticipants(int tournamentId)
        {
            //http://api.challonge.com/v1/documents/participants/index

            //Create RestClient & RestRequest
            RestClient restClient = new RestClient("https://api.challonge.com/v1/");
            RestRequest request = new RestRequest($"tournaments/{tournamentId}/participants.json", Method.GET)
            {
                Credentials = new NetworkCredential(Username, APIKey),
                RequestFormat = DataFormat.Json
            };

            //Pass request
            IRestResponse response = await restClient.ExecuteTaskAsync(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                //Challonge API passed an error [Status Code 422]
                JToken content = JToken.Parse(response.Content);
                if (content.Type != JTokenType.Array && !Helpers.IsNullOrEmpty(content["errors"]))
                    return null;

                List<Participant> returnParticipants = new List<Participant>();
                foreach (JToken participant in content)
                    returnParticipants.Add(participant.First.First.ToObject<Participant>());

                return returnParticipants;
            }

            //Request failed
            return null;
        }

        /// <summary>
        /// Retrieve list of participants from specified tournament URL
        /// </summary>
        public async Task<List<Participant>> GetParticipants(string tournamentUrl, string subDomain = null)
        {
            //http://api.challonge.com/v1/documents/participants/index

            //Create RestClient & RestRequest
            RestClient restClient = new RestClient("https://api.challonge.com/v1/");
            RestRequest request = new RestRequest($"tournaments/{(!string.IsNullOrWhiteSpace(subDomain) ? $"{subDomain}-" : string.Empty)}{tournamentUrl}/participants.json", Method.GET)
            {
                Credentials = new NetworkCredential(Username, APIKey),
                RequestFormat = DataFormat.Json
            };

            //Pass request
            IRestResponse response = await restClient.ExecuteTaskAsync(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                //Challonge API passed an error [Status Code 422]
                JToken content = JToken.Parse(response.Content);
                if (content.Type != JTokenType.Array && !Helpers.IsNullOrEmpty(content["errors"]))
                    return null;

                List<Participant> returnParticipants = new List<Participant>();
                foreach (JToken participant in content)
                    returnParticipants.Add(participant.First.First.ToObject<Participant>());

                return returnParticipants;
            }

            //Request failed
            return null;
        }
    }
}