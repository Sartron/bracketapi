using Newtonsoft.Json.Linq;
using System;
using System.Xml.Linq;

namespace bracketAPI
{
    public static class Helpers
    {
        /// <summary>
        /// Returns a boolean indicating whether or not the JToken is null or empty
        /// </summary>
        public static bool IsNullOrEmpty(JToken jToken)
        {
            return (jToken == null) ||
                   (jToken.Type == JTokenType.Array && !jToken.HasValues) ||
                   (jToken.Type == JTokenType.Object && !jToken.HasValues) ||
                   (jToken.Type == JTokenType.String && jToken.ToString() == String.Empty) ||
                   (jToken.Type == JTokenType.Null);
        }

        /// <summary>
        /// Returns a boolean indicating whether or not the JObject is null or empty
        /// </summary>
        public static bool IsNullOrEmpty(JObject jObject)
        {
            JToken jToken = jObject;
            return (jToken == null) ||
                   (jToken.Type == JTokenType.Array && !jToken.HasValues) ||
                   (jToken.Type == JTokenType.Object && !jToken.HasValues) ||
                   (jToken.Type == JTokenType.String && jToken.ToString() == String.Empty) ||
                   (jToken.Type == JTokenType.Null);
        }

        /// <summary>
        /// Returns a boolean indicating whether or not the XElement is null or empty
        /// </summary>
        public static bool IsNullOrEmpty(XElement xElement)
        {
            return (xElement == null) ||
                (xElement.IsEmpty) ||
                (string.IsNullOrEmpty(xElement.Value));
        }
    }
}