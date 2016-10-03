using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace bracketAPI.Challonge
{
    public class ChallongeBracket
    {
        [JsonProperty("tournament")]
        [XmlElement("tournament")]
        public Bracket Bracket { get; private set; }
    }

    public class ChallongeParticipant
    {
        [JsonProperty("participant")]
        [XmlElement("participant")]
        public Participant Participant { get; private set; }
    }

    public class ChallongeMatch
    {
        [JsonProperty("match")]
        [XmlElement("match")]
        public Match Match { get; private set; }
    }

    public class ChallongeAttachment
    {
        [JsonProperty("match_attachment")]
        [XmlElement("match-attachment")]
        public Attachment Attachment { get; private set; }
    }

    [JsonObject(MemberSerialization.OptIn)]
    public class Bracket
    {
        /// <summary>
        /// ID of the tournament
        /// </summary>
        [JsonProperty("id")]
        [XmlElement("id")]
        public int ID { get; private set; }

        /// <summary>
        /// Name of the tournament
        /// </summary>
        [JsonProperty("name")]
        [XmlElement("name")]
        public string Name { get; private set; }

        /// <summary>
        /// Type of bracket format used in the tournament
        /// </summary>
        [JsonProperty("tournament_type")]
        [JsonConverter(typeof(Enums.TournamentTypeConverter))]
        [XmlIgnore]
        public Enums.TournamentType Type { get; private set; }

        /// <summary>
        /// URL name of the tournament
        /// </summary>
        [JsonProperty("url")]
        [XmlElement("url")]
        public string NameURL { get; private set; }

        /// <summary>
        /// Subdomain hosting the tournament
        /// </summary>
        [JsonProperty("subdomain")]
        [XmlElement("subdomain")]
        public string Subdomain { get; private set; }

        /// <summary>
        /// Description of the tournament
        /// </summary>
        [JsonProperty("description")]
        [XmlElement("description")]
        public string Description { get; private set; }

        /// <summary>
        /// ID of the tournament's game
        /// </summary>
        [JsonProperty("game_id")]
        [XmlElement("game-id")]
        public int? GameID { get; private set; }

        /// <summary>
        /// Name of the tournament's game
        /// </summary>
        [JsonProperty("game_name")]
        [XmlElement("game-name")]
        public string GameName { get; private set; }

        /// <summary>
        /// Progress of the tournament
        /// </summary>
        [JsonProperty("progress_meter")]
        [XmlElement("progress-meter")]
        public int Progress { get; private set; }

        /// <summary>
        /// Determines whether or not the tournament has a sign-up page
        /// </summary>
        [JsonProperty("open_signup")]
        [XmlElement("open-signup")]
        public bool OpenSignup { get; private set; }

        /// <summary>
        /// Determines whether or not the tournament will hold a match between the participants of Losers Semifinals for third place (Single Elimination bracket only)
        /// </summary>
        [JsonProperty("hold_third_place_match")]
        [XmlElement("hold-third-place-match")]
        public bool HoldThirdPlaceMatch { get; private set; }

        /// <summary>
        /// Points awarded for each match win in a Swiss bracket
        /// </summary>
        [JsonProperty("pts_for_match_win")]
        [XmlElement("pts-for-match-win")]
        public double SwissPointsForMatchWin { get; private set; }

        /// <summary>
        /// Points awarded for each match tie in a Swiss bracket
        /// </summary>
        [JsonProperty("pts_for_match_tie")]
        [XmlElement("pts-for-match-tie")]
        public double SwissPointsForMatchTie { get; private set; }

        /// <summary>
        /// Points awarded for each game win in a Swiss bracket
        /// </summary>
        [JsonProperty("pts_for_game_win")]
        [XmlElement("pts-for-game-win")]
        public double SwissPointsForGameWin { get; private set; }

        /// <summary>
        /// Points awarded for each game tie in a Swiss bracket
        /// </summary>
        [JsonProperty("pts_for_game_tie")]
        [XmlElement("pts-for-game-tie")]
        public double SwissPointsForGameTie { get; private set; }

        /// <summary>
        /// Points awarded for each bye in a Swiss bracket
        /// </summary>
        [JsonProperty("pts_for_bye")]
        [XmlElement("pts-for-bye")]
        public double SwissPointsForBye { get; private set; }

        /// <summary>
        /// Number of rounds in a Swiss bracket
        /// </summary>
        [JsonProperty("swiss_rounds")]
        [XmlElement("swiss-rounds")]
        public int SwissRounds { get; private set; }

        /// <summary>
        /// Method used to determine the winner of the Swiss or Round Robin bracket
        /// </summary>
        [JsonProperty("ranked_by")]
        [JsonConverter(typeof(Enums.RankSystemConverter))]
        [XmlIgnore]
        public Enums.RankSystem? RankedBy { get; private set; }

        /// <summary>
        /// Points awarded for each match win in a Round Robin bracket
        /// </summary>
        [JsonProperty("rr_pts_for_match_win")]
        [XmlElement("rr-pts-for-match-win")]
        public double RoundRobinPointsForMatchWin { get; private set; }

        /// <summary>
        /// Points awarded for each match tie in a Round Robin bracket
        /// </summary>
        [JsonProperty("rr_pts_for_match_tie")]
        [XmlElement("rr-pts-for-match-tie")]
        public double RoundRobinPointsForMatchTie { get; private set; }

        /// <summary>
        /// Points awarded for each game win in a Round Robin bracket
        /// </summary>
        [JsonProperty("rr_pts_for_game_win")]
        [XmlElement("rr-pts-for-game-win")]
        public double RoundRobinPointsForGameWin { get; private set; }

        /// <summary>
        /// Points awarded for each game tie in a Round Robin bracket
        /// </summary>
        [JsonProperty("rr_pts_for_game_tie")]
        [XmlElement("rr-pts-for-game-tie")]
        public double RoundRobinPointsForGameTie { get; private set; }

        /// <summary>
        /// Determines whether or not attachments can be uploaded to matches
        /// </summary>
        [JsonProperty("accept_attachments")]
        [XmlElement("accept-attachments")]
        public bool AllowAttachments { get; private set; }

        /// <summary>
        /// Determines whether or not the tournament will include forums
        /// </summary>
        [JsonProperty("hide_forum")]
        [XmlElement("hide-forum")]
        public bool HideForums { get; private set; }

        /// <summary>
        /// Determines whether or not the tournament will display rounds above the bracket (Double Elimination bracket only)
        /// </summary>
        [JsonProperty("show_rounds")]
        [XmlElement("show-rounds")]
        public bool ShowRounds { get; private set; }

        /// <summary>
        /// Determines whether or not the tournament is hidden from the public browsable index and profile
        /// </summary>
        [JsonProperty("private")]
        [XmlElement("private")]
        public bool IsPrivate { get; private set; }

        /// <summary>
        /// Determines whether or not tournament participants will automatically be emailed when a match is available
        /// </summary>
        [JsonProperty("notify_users_when_matches_open")]
        [XmlElement("notify-users-when-matches-open")]
        public bool NotifyUsersWhenMatchIsAvailable { get; private set; }

        /// <summary>
        /// Determines whether or not tournament participants will automatically be emailed when the tournament ends
        /// </summary>
        [JsonProperty("notify_users_when_the_tournament_ends")]
        [XmlElement("notify-users-when-the-tournament-ends")]
        public bool NotifyUsersWhenTournamentIsOver { get; private set; }

        /// <summary>
        /// Determines whether or not the tournament will use sequential pairings as its seeding system rather than traditional seeding
        /// </summary>
        [JsonProperty("sequential_pairings")]
        [XmlElement("sequential-pairings")]
        public bool SequentialPairings { get; private set; }

        /// <summary>
        /// Maximum number of participants allowed in the tournament
        /// </summary>
        [JsonProperty("signup_cap")]
        [XmlElement("signup-cap")]
        public int? SignupCap { get; private set; }

        /// <summary>
        /// The planned or anticipated start time for the tournament
        /// </summary>
        [JsonProperty("start_at")]
        [XmlElement("start-at")]
        public DateTime? StartAt { get; private set; }

        /// <summary>
        /// Length of the participant check-in window in minutes
        /// </summary>
        [JsonProperty("check_in_duration")]
        [XmlElement("check-in-duration")]
        public int? CheckInDuration { get; private set; }

        /// <summary>
        /// Modifies the Grand Finale system of the tournament
        /// </summary>
        [JsonProperty("grand_finals_modifier")]
        [JsonConverter(typeof(Enums.GrandFinalsModifierConverter))]
        [XmlIgnore]
        public Enums.GrandFinalsModifier GrandFinalsModifier { get; private set; }

        /// <summary>
        /// Link to the bracket on Challonge
        /// </summary>
        [JsonProperty("full_challonge_url")]
        [XmlElement("full-challonge-url")]
        public Uri URL { get; private set; }

        /// <summary>
        /// Link to the bracket image
        /// </summary>
        [JsonProperty("live_image_url")]
        [XmlElement("live-image-url")]
        public Uri ImageURL { get; private set; }

        /// <summary>
        /// Time the tournament was created
        /// </summary>
        [JsonProperty("created_at")]
        [XmlElement("created-at")]
        public DateTime CreatedAt { get; private set; }

        /// <summary>
        /// Time the tournament was last updated
        /// </summary>
        [JsonProperty("updated_at")]
        [XmlElement("updated-at")]
        public DateTime UpdatedAt { get; private set; }

        /// <summary>
        /// Time the tournament began
        /// </summary>
        [JsonProperty("started_at")]
        [XmlElement("started-at")]
        public DateTime? StartedAt { get; private set; }

        /// <summary>
        /// Time the tournament ended
        /// </summary>
        [JsonProperty("completed_at")]
        [XmlElement("completed-at")]
        public DateTime? CompletedAt { get; private set; }

        /// <summary>
        /// Current state of the bracket
        /// </summary>
        [JsonProperty("state")]
        [JsonConverter(typeof(Enums.TournamentStateConverter))]
        [XmlIgnore]
        public Enums.TournamentState State { get; private set; }

        /// <summary>
        /// List of matches in the tournament
        /// </summary>
        [JsonProperty("matches")]
        [XmlElement("matches")]
        public List<ChallongeMatch> Matches { get; private set; }

        /// <summary>
        /// List of participants in the tournament
        /// </summary>
        [JsonProperty("participants")]
        [XmlElement("participants")]
        public List<ChallongeParticipant> Participants { get; private set; }

        public override string ToString() { return Name; }
    }

    [JsonObject(MemberSerialization.OptIn)]
    public class Participant
    {
        /// <summary>
        /// Name of the participant
        /// </summary>
        [JsonProperty("name")]
        [XmlElement("name")]
        public string Name { get; private set; }

        /// <summary>
        /// Challonge username of the participant
        /// </summary>
        [JsonProperty("challonge_username")]
        [XmlElement("challonge-username")]
        public string ChallongeUsername { get; private set; }

        /// <summary>
        /// ID of the participant
        /// </summary>
        [JsonProperty("id")]
        [XmlElement("id")]
        public int ID { get; private set; }

        /// <summary>
        /// ID of the tournament
        /// </summary>
        [JsonProperty("tournament_id")]
        [XmlElement("tournament-id")]
        public int TournamentID { get; private set; }

        /// <summary>
        /// Seed of the participant
        /// </summary>
        [JsonProperty("seed")]
        [XmlElement("seed")]
        public int Seed { get; private set; }

        /// <summary>
        /// Final placement within the bracket
        /// </summary>
        [JsonProperty("final_rank")]
        [XmlElement("final-rank")]
        public int? Placing { get; private set; }

        /// <summary>
        /// List of matches in the tournament
        /// </summary>
        [JsonProperty("matches")]
        [XmlElement("matches")]
        public List<ChallongeMatch> Matches { get; private set; }

        public override string ToString() { return Name; }
    }

    [JsonObject(MemberSerialization.OptIn)]
    public class Match
    {
        /// <summary>
        /// ID of the match
        /// </summary>
        [JsonProperty("id")]
        [XmlElement("id")]
        public int ID { get; private set; }

        /// <summary>
        /// Player 1 ID of the match
        /// </summary>
        [JsonProperty("player1_id")]
        [XmlElement("player1-id")]
        public int? Player1ID { get; private set; }

        /// <summary>
        /// Player 2 ID of the match
        /// </summary>
        [JsonProperty("player2_id")]
        [XmlElement("player2-id")]
        public int? Player2ID { get; private set; }

        /// <summary>
        /// ID of player who lost the match
        /// </summary>
        [JsonProperty("loser_id")]
        [XmlElement("loser-id")]
        public int? LoserPlayerID { get; private set; }

        /// <summary>
        /// ID of player who won the match
        /// </summary>
        [JsonProperty("winner_id")]
        [XmlElement("winner-id")]
        public int? WinnerPlayerID { get; private set; }

        /// <summary>
        /// ID of player who won the match
        /// </summary>
        [JsonProperty("round")]
        [XmlElement("round")]
        public int Round { get; private set; }

        /// <summary>
        /// Current state of the bracket
        /// </summary>
        [JsonProperty("state")]
        [JsonConverter(typeof(Enums.MatchStateConverter))]
        [XmlIgnore]
        public Enums.MatchState State { get; private set; }

        /// <summary>
        /// Match score
        /// </summary>
        [JsonProperty("scores_csv")]
        [XmlElement("scores-csv")]
        public string Score { get; private set; }

        /// <summary>
        /// List of attachments attached to the match
        /// </summary>
        [JsonProperty("attachments")]
        [XmlElement("attachments")]
        public List<ChallongeAttachment> Attachments { get; private set; }
    }

    [JsonObject(MemberSerialization.OptIn)]
    public class Attachment
    {
        /// <summary>
        /// ID of the attachment
        /// </summary>
        [JsonProperty("id")]
        [XmlElement("id")]
        public int ID { get; private set; }

        /// <summary>
        /// Match ID that the attachment is attached to
        /// </summary>
        [JsonProperty("match_id")]
        [XmlElement("match-id")]
        public int MatchID { get; private set; }

        /// <summary>
        /// Description of the attachment
        /// </summary>
        [JsonProperty("description")]
        [XmlElement("description")]
        public string Description { get; private set; }
    }
}