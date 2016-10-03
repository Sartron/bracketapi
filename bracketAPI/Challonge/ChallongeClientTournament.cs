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
        /// Retrieve tournament from specified ID
        /// </summary>
        public async Task<Bracket> GetTournament(int tournamentId, bool includeParticipants = false, bool includeMatches = false)
        {
            //http://api.challonge.com/v1/documents/tournaments/show

            //Create RestClient & RestRequest
            RestClient restClient = new RestClient("https://api.challonge.com/v1/");
            RestRequest request = new RestRequest($"tournaments/{tournamentId}.json", Method.GET)
            {
                Credentials = new NetworkCredential(Username, APIKey),
                Parameters =
                {
                    new Parameter
                    {
                        Name = "include_participants",
                        Value = Convert.ToSingle(includeParticipants),
                        ContentType = null,
                        Type = ParameterType.GetOrPost
                    },
                    new Parameter
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
                return Helpers.IsNullOrEmpty(JToken.Parse(response.Content)["errors"]) ? JToken.Parse(response.Content)["tournament"].ToObject<Bracket>() : null;

            //Request failed
            return null;
        }

        /// <summary>
        /// Retrieve tournament from specified tournament URL
        /// </summary>
        public async Task<Bracket> GetTournament(string tournamentUrl, string subDomain = null, bool includeParticipants = false, bool includeMatches = false)
        {
            //http://api.challonge.com/v1/documents/tournaments/show

            //Create RestClient & RestRequest
            RestClient restClient = new RestClient("https://api.challonge.com/v1/");
            RestRequest request = new RestRequest($"tournaments/{(!string.IsNullOrWhiteSpace(subDomain) ? $"{subDomain}-" : string.Empty)}{tournamentUrl}.json", Method.GET)
            {
                Credentials = new NetworkCredential(Username, APIKey),
                Parameters =
                {
                    new Parameter
                    {
                        Name = "include_participants",
                        Value = Convert.ToSingle(includeParticipants),
                        ContentType = null,
                        Type = ParameterType.GetOrPost
                    },
                    new Parameter
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
                return Helpers.IsNullOrEmpty(JToken.Parse(response.Content)["errors"]) ? JToken.Parse(response.Content)["tournament"].ToObject<Bracket>() : null;


            //Request failed
            return null;
        }

        ///// <summary>
        ///// Creates a new tournament following the specified parameters
        ///// </summary>
        //public async Task<bool> CreateTournament(string name, string url)
        //{
        //    //http://api.challonge.com/v1/documents/tournaments/create

        //    //Create RestClient & RestRequest
        //    RestClient restClient = new RestClient("https://api.challonge.com/v1/");
        //    RestRequest request = new RestRequest($"tournaments.json", Method.POST)
        //    {
        //        Credentials = new NetworkCredential(Username, APIKey),
        //        Parameters =
        //        {
        //            new Parameter
        //            {
        //                Name = "tournament[name]",
        //                Value = name,
        //                ContentType = null,
        //                Type = ParameterType.GetOrPost
        //            },
        //            new Parameter
        //            {
        //                Name = "tournament[url]",
        //                Value = url,
        //                ContentType = null,
        //                Type = ParameterType.GetOrPost
        //            }
        //        },
        //        RequestFormat = DataFormat.Json
        //    };

        //    //Pass request
        //    IRestResponse response = await restClient.ExecuteTaskAsync(request);
        //    if (response.StatusCode == HttpStatusCode.OK)
        //        return true;

        //    //Request failed
        //    return false;
        //}

        /// <summary>
        /// Retrieve list of tournaments falling under specified parameters
        /// </summary>
        public async Task<List<Bracket>> GetTournaments(string subDomain = null, TournamentState state = TournamentState.All, TournamentType type = TournamentType.All, DateTime createdAfter = default(DateTime), DateTime createdBefore = default(DateTime))
        {
            //http://api.challonge.com/v1/documents/tournaments/index

            //Create RestClient & RestRequest
            RestClient restClient = new RestClient("https://api.challonge.com/v1/");
            RestRequest request = new RestRequest($"tournaments.json", Method.GET)
            {
                Credentials = new NetworkCredential(Username, APIKey),
                Parameters =
                {
                    new Parameter
                    {
                        Name = "state",
                        Value = Converters.TournamentStateConverter(state),
                        ContentType = null,
                        Type = ParameterType.GetOrPost
                    },
                    new Parameter
                    {
                        Name = "created_after",
                        Value = createdAfter.ToString("yyyy-MM-dd"),
                        ContentType = null,
                        Type = ParameterType.GetOrPost
                    },
                    new Parameter
                    {
                        Name = "created_before",
                        Value = !createdBefore.Equals(default(DateTime)) ? createdBefore.ToString("yyyy-MM-dd") : DateTime.Now.ToString("yyyy-MM-dd"),
                        ContentType = null,
                        Type = ParameterType.GetOrPost
                    }
                },
                RequestFormat = DataFormat.Json
            };
            if (!string.IsNullOrWhiteSpace(Converters.TournamentTypeConverter(type)))
                request.AddParameter("type", Converters.TournamentTypeConverter(type), null, ParameterType.GetOrPost);
            if (!string.IsNullOrWhiteSpace(subDomain))
                request.AddParameter("subdomain", subDomain, null, ParameterType.GetOrPost);

            //Pass request
            IRestResponse response = await restClient.ExecuteTaskAsync(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                //Challonge API passed an error [Status Code 422]
                JToken content = JToken.Parse(response.Content);
                if (content.Type != JTokenType.Array && !Helpers.IsNullOrEmpty(content["errors"]))
                    return null;

                List<Bracket> returnTournaments = new List<Bracket>();
                foreach (JToken tournament in content)
                    returnTournaments.Add(tournament.First.First.ToObject<Bracket>());

                return returnTournaments;
            }

            //Request failed
            return null;
        }
    }
}