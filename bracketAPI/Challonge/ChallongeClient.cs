namespace bracketAPI.Challonge
{
    /// <summary>
    /// C# Wrapper used to access Challonge API
    /// </summary>
    public partial class ChallongeClient
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
    }
}