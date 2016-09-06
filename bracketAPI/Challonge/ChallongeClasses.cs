using RestSharp.Deserializers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace bracketAPI.Challonge
{
    public class ChallongeTournament
    {
        [DeserializeAs(Name = "id")]
        public int ID { get; set; }

        [DeserializeAs(Name = "name")]
        public string Name { get; set; }

        [DeserializeAs(Name = "url")]
        public string URL { get; set; }

        [DeserializeAs(Name = "full_challonge_url")]
        public string FullURL { get; set; }

        [DeserializeAs(Name = "description")]
        public string Description { get; set; }

        [DeserializeAs(Name = "created_at")]
        public DateTime Created { get; set; }

        [DeserializeAs(Name = "started_at")]
        public DateTime Started { get; set; }

        [DeserializeAs(Name = "completed_at")]
        public DateTime Ended { get; set; }

        [DeserializeAs(Name = "progress_meter")]
        public int Progress { get; set; }

        [DeserializeAs(Name = "game_id")]
        public int GameID { get; set; }

        [DeserializeAs(Name = "subdomain")]
        public string Subdomain { get; set; }

        [DeserializeAs(Name = "live_image_url")]
        public Uri Image { get; set; }

        [DeserializeAs(Name = "game_name")]
        public string Game { get; set; }

        public ChallongeTournament(string data, DataFormat dataFormat)
        {
            if (dataFormat == DataFormat.JSON)
                ParseJSON(data);
            else if (dataFormat == DataFormat.XML)
                ParseXML(data);
        }

        public void ParseJSON(string json)
        {
            JObject jObject = JObject.Parse(json);
            Debug.WriteLine("Hi");
        }

        public void ParseXML(string XML)
        {
            
        }
    }
}