using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
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
                    new Parameter() { Name = "include_participants", Value = Convert.ToSingle(includeParticipants), ContentType = null, Type = ParameterType.GetOrPost },
                    new Parameter() { Name = "include_matches", Value = Convert.ToSingle(includeMatches), ContentType = null, Type = ParameterType.GetOrPost }
                },
                RequestFormat = (RestSharp.DataFormat)dataFormat
            };

            //Pass request
            IRestResponse response = await restClient.ExecuteTaskAsync(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                if (dataFormat == DataFormat.JSON)
                    return new ChallongeTournament(JObject.Parse(response.Content));
                else if (dataFormat == DataFormat.XML)
                    return new ChallongeTournament(XDocument.Parse(response.Content).Root);
            }

            //Request failed
            return null;
        }

        /// <summary>
        /// Retrieve tournament from specified tournament URL
        /// </summary>
        public async Task<ChallongeTournament> GetTournament(string tournamentUrl, string subDomain = "", bool includeParticipants = false, bool includeMatches = false, DataFormat dataFormat = DataFormat.JSON)
        {
            //http://api.challonge.com/v1/documents/tournaments/show

            //Create RestClient & RestRequest
            RestClient restClient = new RestClient("https://api.challonge.com/v1/");
            RestRequest request = new RestRequest($"tournaments/{(!string.IsNullOrWhiteSpace(subDomain) ? $"{subDomain}-" : string.Empty)}{tournamentUrl}.{dataFormat.ToString().ToLower()}", Method.GET)
            {
                Credentials = new NetworkCredential(Username, APIKey),
                Parameters =
                {
                    new Parameter() { Name = "include_participants", Value = Convert.ToSingle(includeParticipants), ContentType = null, Type = ParameterType.GetOrPost },
                    new Parameter() { Name = "include_matches", Value = Convert.ToSingle(includeMatches), ContentType = null, Type = ParameterType.GetOrPost }
                },
                RequestFormat = (RestSharp.DataFormat)dataFormat
            };

            //Pass request
            IRestResponse response = await restClient.ExecuteTaskAsync(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                if (dataFormat == DataFormat.JSON)
                    return new ChallongeTournament(JObject.Parse(response.Content));
                else if (dataFormat == DataFormat.XML)
                    return new ChallongeTournament(XDocument.Parse(response.Content).Root);
            }

            //Request failed
            return null;
        }

        /// <summary>
        /// Retrieve list of tournaments falling under specified parameters
        /// </summary>
        public async Task<List<ChallongeTournament>> GetTournaments(TournamentState state = TournamentState.All, TournamentType type = TournamentType.All, DateTime createdAfter = default(DateTime), DateTime createdBefore = default(DateTime), string subDomain = "", DataFormat dataFormat = DataFormat.JSON)
        {
            //http://api.challonge.com/v1/documents/tournaments/index
            List<ChallongeTournament> returnTournaments = new List<ChallongeTournament>();

            //Create RestClient & RestRequest
            RestClient restClient = new RestClient("https://api.challonge.com/v1/");
            RestRequest request = new RestRequest($"tournaments.{dataFormat.ToString().ToLower()}", Method.GET)
            {
                Credentials = new NetworkCredential(Username, APIKey),
                Parameters =
                {
                    new Parameter() { Name = "state", Value = ChallongeHelpers.ParseEnum(state), ContentType = null, Type = ParameterType.GetOrPost },
                    new Parameter() { Name = "created_after", Value = createdAfter.ToString("yyyy-MM-dd"), ContentType = null, Type = ParameterType.GetOrPost },
                    new Parameter() { Name = "created_before", Value = !createdBefore.Equals(default(DateTime)) ? createdBefore.ToString("yyyy-MM-dd") : DateTime.Now.ToString("yyyy-MM-dd"), ContentType = null, Type = ParameterType.GetOrPost }
                },
                RequestFormat = (RestSharp.DataFormat)dataFormat
            };
            if (!string.IsNullOrWhiteSpace(ChallongeHelpers.ParseEnum(type)))
                request.AddParameter("type", ChallongeHelpers.ParseEnum(type), null, ParameterType.GetOrPost);
            if (!string.IsNullOrWhiteSpace(subDomain))
                request.AddParameter("subdomain", subDomain, null, ParameterType.GetOrPost);

            //Pass request
            IRestResponse response = await restClient.ExecuteTaskAsync(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                if (dataFormat == DataFormat.JSON)
                {
                    foreach (JObject tournament in JArray.Parse(response.Content))
                        returnTournaments.Add(new ChallongeTournament(tournament));
                }
                else if (dataFormat == DataFormat.XML)
                {
                    foreach (XElement tournament in XDocument.Parse(response.Content).Root.Elements())
                        returnTournaments.Add(new ChallongeTournament(tournament));
                }

                return returnTournaments;
            }

            //Request failed
            return null;
        }

        /// <summary>
        /// Retrieve participant from specified tournament ID
        /// </summary>
        public async Task<ChallongeParticipant> GetParticipant(int tournamentId, int participantId, bool includeMatches = false, DataFormat dataFormat = DataFormat.JSON)
        {
            //http://api.challonge.com/v1/documents/participants/show

            //Create RestClient & RestRequest
            RestClient restClient = new RestClient("https://api.challonge.com/v1/");
            RestRequest request = new RestRequest($"tournaments/{tournamentId}/participants/{participantId}.{dataFormat.ToString().ToLower()}", Method.GET)
            {
                Credentials = new NetworkCredential(Username, APIKey),
                Parameters = { new Parameter() { Name = "include_matches", Value = Convert.ToSingle(includeMatches), ContentType = null, Type = ParameterType.GetOrPost } },
                RequestFormat = (RestSharp.DataFormat)dataFormat
            };

            //Pass request
            IRestResponse response = await restClient.ExecuteTaskAsync(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                if (dataFormat == DataFormat.JSON)
                    return new ChallongeParticipant(JObject.Parse(response.Content));
                else if (dataFormat == DataFormat.XML)
                    return new ChallongeParticipant(XDocument.Parse(response.Content).Root);
            }

            //Request failed
            return null;
        }

        /// <summary>
        /// Retrieve participant from specified tournament URL
        /// </summary>
        public async Task<ChallongeParticipant> GetParticipant(string tournamentUrl, int participantId, string subDomain = "", bool includeMatches = false, DataFormat dataFormat = DataFormat.JSON)
        {
            //http://api.challonge.com/v1/documents/participants/show

            //Create RestClient & RestRequest
            RestClient restClient = new RestClient("https://api.challonge.com/v1/");
            RestRequest request = new RestRequest($"tournaments/{(!string.IsNullOrWhiteSpace(subDomain) ? $"{subDomain}-" : string.Empty)}{tournamentUrl}/participants/{participantId}.{dataFormat.ToString().ToLower()}", Method.GET)
            {
                Credentials = new NetworkCredential(Username, APIKey),
                Parameters = { new Parameter() { Name = "include_matches", Value = Convert.ToSingle(includeMatches), ContentType = null, Type = ParameterType.GetOrPost } },
                RequestFormat = (RestSharp.DataFormat)dataFormat
            };

            //Pass request
            IRestResponse response = await restClient.ExecuteTaskAsync(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                if (dataFormat == DataFormat.JSON)
                    return new ChallongeParticipant(JObject.Parse(response.Content));
                else if (dataFormat == DataFormat.XML)
                    return new ChallongeParticipant(XDocument.Parse(response.Content).Root);
            }

            //Request failed
            return null;
        }

        /// <summary>
        /// Retrieve list of participants from specified tournament ID
        /// </summary>
        public async Task<List<ChallongeParticipant>> GetParticipants(int tournamentId, DataFormat dataFormat = DataFormat.JSON)
        {
            //http://api.challonge.com/v1/documents/participants/index
            List<ChallongeParticipant> returnParticipants = new List<ChallongeParticipant>();

            //Create RestClient & RestRequest
            RestClient restClient = new RestClient("https://api.challonge.com/v1/");
            RestRequest request = new RestRequest($"tournaments/{tournamentId}/participants.{dataFormat.ToString().ToLower()}", Method.GET)
            {
                Credentials = new NetworkCredential(Username, APIKey),
                RequestFormat = (RestSharp.DataFormat)dataFormat
            };

            //Pass request
            IRestResponse response = await restClient.ExecuteTaskAsync(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                if (dataFormat == DataFormat.JSON)
                {
                    foreach (JObject participant in JArray.Parse(response.Content))
                        returnParticipants.Add(new ChallongeParticipant(participant));
                }
                else if (dataFormat == DataFormat.XML)
                {
                    foreach (XElement participant in XDocument.Parse(response.Content).Root.Elements())
                        returnParticipants.Add(new ChallongeParticipant(participant));
                }

                return returnParticipants;
            }

            //Request failed
            return null;
        }

        /// <summary>
        /// Retrieve list of participants from specified tournament URL
        /// </summary>
        public async Task<List<ChallongeParticipant>> GetParticipants(string tournamentUrl, string subDomain = "", DataFormat dataFormat = DataFormat.JSON)
        {
            //http://api.challonge.com/v1/documents/participants/index
            List<ChallongeParticipant> returnParticipants = new List<ChallongeParticipant>();

            //Create RestClient & RestRequest
            RestClient restClient = new RestClient("https://api.challonge.com/v1/");
            RestRequest request = new RestRequest($"tournaments/{(!string.IsNullOrWhiteSpace(subDomain) ? $"{subDomain}-" : string.Empty)}{tournamentUrl}/participants.{dataFormat.ToString().ToLower()}", Method.GET)
            {
                Credentials = new NetworkCredential(Username, APIKey),
                RequestFormat = (RestSharp.DataFormat)dataFormat
            };

            //Pass request
            IRestResponse response = await restClient.ExecuteTaskAsync(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                if (dataFormat == DataFormat.JSON)
                {
                    foreach (JObject participant in JArray.Parse(response.Content))
                        returnParticipants.Add(new ChallongeParticipant(participant));
                }
                else if (dataFormat == DataFormat.XML)
                {
                    foreach (XElement participant in XDocument.Parse(response.Content).Root.Elements())
                        returnParticipants.Add(new ChallongeParticipant(participant));
                }

                return returnParticipants;
            }

            //Request failed
            return null;
        }

        /// <summary>
        /// Retrieve match from specified tournament ID
        /// </summary>
        public async Task<ChallongeMatch> GetMatch(int tournamentId, int matchId, bool includeAttachments = false, DataFormat dataFormat = DataFormat.JSON)
        {
            //http://api.challonge.com/v1/documents/matches/show

            //Create RestClient & RestRequest
            RestClient restClient = new RestClient("https://api.challonge.com/v1/");
            RestRequest request = new RestRequest($"tournaments/{tournamentId}/matches/{matchId}.{dataFormat.ToString().ToLower()}", Method.GET)
            {
                Credentials = new NetworkCredential(Username, APIKey),
                Parameters = { new Parameter() { Name = "include_attachments", Value = Convert.ToSingle(includeAttachments), ContentType = null, Type = ParameterType.GetOrPost } },
                RequestFormat = (RestSharp.DataFormat)dataFormat
            };

            //Pass request
            IRestResponse response = await restClient.ExecuteTaskAsync(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                if (dataFormat == DataFormat.JSON)
                    return new ChallongeMatch(JObject.Parse(response.Content));
                else if (dataFormat == DataFormat.XML)
                    return new ChallongeMatch(XDocument.Parse(response.Content).Root);
            }

            //Request failed
            return null;
        }

        /// <summary>
        /// Retrieve match from specified tournament URL
        /// </summary>
        public async Task<ChallongeMatch> GetMatch(string tournamentUrl, int matchId, string subDomain = "", bool includeAttachments = false, DataFormat dataFormat = DataFormat.JSON)
        {
            //http://api.challonge.com/v1/documents/matches/show

            //Create RestClient & RestRequest
            RestClient restClient = new RestClient("https://api.challonge.com/v1/");
            RestRequest request = new RestRequest($"tournaments/{(!string.IsNullOrWhiteSpace(subDomain) ? $"{subDomain}-" : string.Empty)}{tournamentUrl}/matches/{matchId}.{dataFormat.ToString().ToLower()}", Method.GET)
            {
                Credentials = new NetworkCredential(Username, APIKey),
                Parameters = { new Parameter() { Name = "include_attachments", Value = Convert.ToSingle(includeAttachments), ContentType = null, Type = ParameterType.GetOrPost } },
                RequestFormat = (RestSharp.DataFormat)dataFormat
            };

            //Pass request
            IRestResponse response = await restClient.ExecuteTaskAsync(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                if (dataFormat == DataFormat.JSON)
                    return new ChallongeMatch(JObject.Parse(response.Content));
                else if (dataFormat == DataFormat.XML)
                    return new ChallongeMatch(XDocument.Parse(response.Content).Root);
            }

            //Request failed
            return null;
        }

        /// <summary>
        /// Retrieve list of matches from specified tournament ID
        /// </summary>
        public async Task<List<ChallongeMatch>> GetMatches(int tournamentId, MatchState state = MatchState.All, int participantId = -1, DataFormat dataFormat = DataFormat.JSON)
        {
            //http://api.challonge.com/v1/documents/matches/index
            List<ChallongeMatch> returnMatches = new List<ChallongeMatch>();

            //Create RestClient & RestRequest
            RestClient restClient = new RestClient("https://api.challonge.com/v1/");
            RestRequest request = new RestRequest($"tournaments/{tournamentId}/matches.{dataFormat.ToString().ToLower()}", Method.GET)
            {
                Credentials = new NetworkCredential(Username, APIKey),
                Parameters = { new Parameter() { Name = "state", Value = state.ToString().ToLower(), ContentType = null, Type = ParameterType.GetOrPost } },
                RequestFormat = (RestSharp.DataFormat)dataFormat
            };
            if (participantId >= 0)
                request.AddParameter("participant_id", participantId, null, ParameterType.GetOrPost);

            //Pass request
            IRestResponse response = await restClient.ExecuteTaskAsync(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                if (dataFormat == DataFormat.JSON)
                {
                    foreach (JObject match in JArray.Parse(response.Content))
                        returnMatches.Add(new ChallongeMatch(match));
                }
                else if (dataFormat == DataFormat.XML)
                {
                    foreach (XElement match in XDocument.Parse(response.Content).Root.Elements())
                        returnMatches.Add(new ChallongeMatch(match));
                }

                return returnMatches;
            }

            //Request failed
            return null;
        }

        /// <summary>
        /// Retrieve list of matches from specified tournament URL
        /// </summary>
        public async Task<List<ChallongeMatch>> GetMatches(string tournamentUrl, string subDomain = "", MatchState state = MatchState.All, int participantId = -1, DataFormat dataFormat = DataFormat.JSON)
        {
            //http://api.challonge.com/v1/documents/matches/index
            List<ChallongeMatch> returnMatches = new List<ChallongeMatch>();

            //Create RestClient & RestRequest
            RestClient restClient = new RestClient("https://api.challonge.com/v1/");
            RestRequest request = new RestRequest($"tournaments/{(!string.IsNullOrWhiteSpace(subDomain) ? $"{subDomain}-" : string.Empty)}{tournamentUrl}/matches.{dataFormat.ToString().ToLower()}", Method.GET)
            {
                Credentials = new NetworkCredential(Username, APIKey),
                Parameters = { new Parameter() { Name = "state", Value = state.ToString().ToLower(), ContentType = null, Type = ParameterType.GetOrPost } },
                RequestFormat = (RestSharp.DataFormat)dataFormat
            };
            if (participantId >= 0)
                request.AddParameter("participant_id", participantId, null, ParameterType.GetOrPost);

            //Pass request
            IRestResponse response = await restClient.ExecuteTaskAsync(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                if (dataFormat == DataFormat.JSON)
                {
                    foreach (JObject match in JArray.Parse(response.Content))
                        returnMatches.Add(new ChallongeMatch(match));
                }
                else if (dataFormat == DataFormat.XML)
                {
                    foreach (XElement match in XDocument.Parse(response.Content).Root.Elements())
                        returnMatches.Add(new ChallongeMatch(match));
                }

                return returnMatches;
            }

            //Request failed
            return null;
        }

        /// <summary>
        /// Retrieve match attachment from specified tournament ID
        /// </summary>
        public async Task<ChallongeAttachment> GetMatchAttachment(int tournamentId, int matchId, int attachmentId, DataFormat dataFormat = DataFormat.JSON)
        {
            //http://api.challonge.com/v1/documents/match_attachments/show

            //Create RestClient & RestRequest
            RestClient restClient = new RestClient("https://api.challonge.com/v1/");
            RestRequest request = new RestRequest($"tournaments/{tournamentId}/matches/{matchId}/attachments/{attachmentId}.{dataFormat.ToString().ToLower()}", Method.GET)
            {
                Credentials = new NetworkCredential(Username, APIKey),
                RequestFormat = (RestSharp.DataFormat)dataFormat
            };

            //Pass request
            IRestResponse response = await restClient.ExecuteTaskAsync(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                if (dataFormat == DataFormat.JSON)
                    return new ChallongeAttachment(JObject.Parse(response.Content));
                else if (dataFormat == DataFormat.XML)
                    return new ChallongeAttachment(XDocument.Parse(response.Content).Root);
            }

            //Request failed
            return null;
        }

        /// <summary>
        /// Retrieve match attachment from specified tournament URL
        /// </summary>
        public async Task<ChallongeAttachment> GetMatchAttachment(string tournamentUrl, int matchId, int attachmentId, string subDomain = "", DataFormat dataFormat = DataFormat.JSON)
        {
            //http://api.challonge.com/v1/documents/match_attachments/show

            //Create RestClient & RestRequest
            RestClient restClient = new RestClient("https://api.challonge.com/v1/");
            RestRequest request = new RestRequest($"tournaments/{(!string.IsNullOrWhiteSpace(subDomain) ? $"{subDomain}-" : string.Empty)}{tournamentUrl}/matches/{matchId}/attachments/{attachmentId}.{dataFormat.ToString().ToLower()}", Method.GET)
            {
                Credentials = new NetworkCredential(Username, APIKey),
                RequestFormat = (RestSharp.DataFormat)dataFormat
            };

            //Pass request
            IRestResponse response = await restClient.ExecuteTaskAsync(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                if (dataFormat == DataFormat.JSON)
                    return new ChallongeAttachment(JObject.Parse(response.Content));
                else if (dataFormat == DataFormat.XML)
                    return new ChallongeAttachment(XDocument.Parse(response.Content).Root);
            }

            //Request failed
            return null;
        }

        /// <summary>
        /// Retrieve list of match attachments from specified tournament ID
        /// </summary>
        public async Task<List<ChallongeAttachment>> GetMatchAttachments(int tournamentId, int matchId, DataFormat dataFormat = DataFormat.JSON)
        {
            //http://api.challonge.com/v1/documents/match_attachments/index
            List<ChallongeAttachment> returnMatches = new List<ChallongeAttachment>();

            //Create RestClient & RestRequest
            RestClient restClient = new RestClient("https://api.challonge.com/v1/");
            RestRequest request = new RestRequest($"tournaments/{tournamentId}/matches/{matchId}/attachments.{dataFormat.ToString().ToLower()}", Method.GET)
            {
                Credentials = new NetworkCredential(Username, APIKey),
                RequestFormat = (RestSharp.DataFormat)dataFormat
            };

            //Pass request
            IRestResponse response = await restClient.ExecuteTaskAsync(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                if (dataFormat == DataFormat.JSON)
                {
                    foreach (JObject attachment in JArray.Parse(response.Content))
                        returnMatches.Add(new ChallongeAttachment(attachment));
                }
                else if (dataFormat == DataFormat.XML)
                {
                    foreach (XElement attachment in XDocument.Parse(response.Content).Root.Elements())
                        returnMatches.Add(new ChallongeAttachment(attachment));
                }

                return returnMatches;
            }

            //Request failed
            return null;
        }

        /// <summary>
        /// Retrieve list of match attachments from specified tournament URL
        /// </summary>
        public async Task<List<ChallongeAttachment>> GetMatchAttachments(string tournamentUrl, int matchId, string subDomain = "", DataFormat dataFormat = DataFormat.JSON)
        {
            //http://api.challonge.com/v1/documents/match_attachments/index
            List<ChallongeAttachment> returnMatches = new List<ChallongeAttachment>();

            //Create RestClient & RestRequest
            RestClient restClient = new RestClient("https://api.challonge.com/v1/");
            RestRequest request = new RestRequest($"tournaments/{(!string.IsNullOrWhiteSpace(subDomain) ? $"{subDomain}-" : string.Empty)}{tournamentUrl}/matches/{matchId}/attachments.{dataFormat.ToString().ToLower()}", Method.GET)
            {
                Credentials = new NetworkCredential(Username, APIKey),
                RequestFormat = (RestSharp.DataFormat)dataFormat
            };

            //Pass request
            IRestResponse response = await restClient.ExecuteTaskAsync(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                if (dataFormat == DataFormat.JSON)
                {
                    foreach (JObject attachment in JArray.Parse(response.Content))
                        returnMatches.Add(new ChallongeAttachment(attachment));
                }
                else if (dataFormat == DataFormat.XML)
                {
                    foreach (XElement attachment in XDocument.Parse(response.Content).Root.Elements())
                        returnMatches.Add(new ChallongeAttachment(attachment));
                }

                return returnMatches;
            }

            //Request failed
            return null;
        }
    }
}