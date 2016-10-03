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
        /// Retrieve attachment from specified attachment ID
        /// </summary>
        public async Task<Attachment> GetAttachment(int tournamentId, int matchId, int attachmentId)
        {
            //http://api.challonge.com/v1/documents/match_attachments/show

            //Create RestClient & RestRequest
            RestClient restClient = new RestClient("https://api.challonge.com/v1/");
            RestRequest request = new RestRequest($"tournaments/{tournamentId}/matches/{matchId}/attachments/{attachmentId}.json", Method.GET)
            {
                Credentials = new NetworkCredential(Username, APIKey),
                RequestFormat = DataFormat.Json
            };

            //Pass request
            IRestResponse response = await restClient.ExecuteTaskAsync(request);
            if (response.StatusCode == HttpStatusCode.OK)
                return Helpers.IsNullOrEmpty(JToken.Parse(response.Content)["errors"]) ? JToken.Parse(response.Content)["match_attachment"].ToObject<Attachment>() : null;

            //Request failed
            return null;
        }

        /// <summary>
        /// Retrieve attachment from specified tournament URL
        /// </summary>
        public async Task<Attachment> GetAttachment(string tournamentUrl, int matchId, int attachmentId, string subDomain = null)
        {
            //http://api.challonge.com/v1/documents/match_attachments/show

            //Create RestClient & RestRequest
            RestClient restClient = new RestClient("https://api.challonge.com/v1/");
            RestRequest request = new RestRequest($"tournaments/{(!string.IsNullOrWhiteSpace(subDomain) ? $"{subDomain}-" : string.Empty)}{tournamentUrl}/matches/{matchId}/attachments/{attachmentId}.json", Method.GET)
            {
                Credentials = new NetworkCredential(Username, APIKey),
                RequestFormat = DataFormat.Json
            };

            //Pass request
            IRestResponse response = await restClient.ExecuteTaskAsync(request);
            if (response.StatusCode == HttpStatusCode.OK)
                return Helpers.IsNullOrEmpty(JToken.Parse(response.Content)["errors"]) ? JToken.Parse(response.Content)["match_attachment"].ToObject<Attachment>() : null;

            //Request failed
            return null;
        }

        /// <summary>
        /// Retrieve list of attachments from specified match ID
        /// </summary>
        public async Task<List<Attachment>> GetAttachments(int tournamentId, int matchId)
        {
            //http://api.challonge.com/v1/documents/match_attachments/index

            //Create RestClient & RestRequest
            RestClient restClient = new RestClient("https://api.challonge.com/v1/");
            RestRequest request = new RestRequest($"tournaments/{tournamentId}/matches/{matchId}/attachments.json", Method.GET)
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

                List<Attachment> returnAttachments = new List<Attachment>();
                foreach (JToken attachment in content)
                    returnAttachments.Add(attachment.First.First.ToObject<Attachment>());

                return returnAttachments;
            }

            //Request failed
            return null;
        }

        /// <summary>
        /// Retrieve list of attachments from specified match ID
        /// </summary>
        public async Task<List<Attachment>> GetAttachments(string tournamentUrl, int matchId, string subDomain = null)
        {
            //http://api.challonge.com/v1/documents/match_attachments/index

            //Create RestClient & RestRequest
            RestClient restClient = new RestClient("https://api.challonge.com/v1/");
            RestRequest request = new RestRequest($"tournaments/{(!string.IsNullOrWhiteSpace(subDomain) ? $"{subDomain}-" : string.Empty)}{tournamentUrl}/matches/{matchId}/attachments.json", Method.GET)
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

                List<Attachment> returnAttachments = new List<Attachment>();
                foreach (JToken attachment in content)
                    returnAttachments.Add(attachment.First.First.ToObject<Attachment>());

                return returnAttachments;
            }

            //Request failed
            return null;
        }
    }
}