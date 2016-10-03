using Newtonsoft.Json;
using System;

namespace bracketAPI.Challonge.Enums
{
    /// <summary>
    /// Bracket format of tournament
    /// </summary>
    public enum TournamentType
    {
        /// <summary>
        /// Participants are removed from the tournament upon elimination
        /// </summary>
        SingleElimination,
        /// <summary>
        /// Participants are removed from the winner bracket upon elimination and continue in the losers bracket where they may be removed upon repeated elimination
        /// </summary>
        DoubleElimination,
        /// <summary>
        /// Participants compete with each opponent within the bracket
        /// </summary>
        RoundRobin,
        /// <summary>
        /// Participants compete within a series of rounds against one opponent each
        /// </summary>
        Swiss
    }

    /// <summary>
    /// Represents the current state of the tournament
    /// </summary>
    public enum TournamentState
    {
        /// <summary>
        /// Has not started
        /// </summary>
        Pending,
        /// <summary>
        /// Is currently running
        /// </summary>
        Underway,
        /// <summary>
        /// Bracket is complete, but not formally ended
        /// </summary>
        AwaitingReview,
        /// <summary>
        /// Bracket is over and finalized
        /// </summary>
        Complete
    }

    /// <summary>
    /// Methods for determing the winner of a Round Robin or Swiss bracket
    /// </summary>
    public enum RankSystem
    {
        /// <summary>
        /// Winner determined by the number of matches won
        /// </summary>
        MatchWins,
        /// <summary>
        /// Winner determined by the number of games won
        /// </summary>
        GameWins,
        /// <summary>
        /// Winner determined by the percentage of games won
        /// </summary>
        GameWinPercentage,
        /// <summary>
        /// Winner determined by the number of points scored
        /// </summary>
        PointsScored,
        /// <summary>
        /// Winner determined by the net number of points scored
        /// </summary>
        PointsDifference,
        /// <summary>
        /// Round Robin only
        /// </summary>
        Custom
    }

    /// <summary>
    /// Modifies the Grand Finale system of the tournament
    /// </summary>
    public enum GrandFinalsModifier
    {
        /// <summary>
        /// Winner gets two sets to win Grand Finals
        /// </summary>
        TwoSets,
        /// <summary>
        /// Both the winner and loser get one set to win Grand Finals
        /// </summary>
        OneSet,
        /// <summary>
        /// Winner of Losers Finals and Winners Finals do not play Grand Finals
        /// </summary>
        None
    }

    /// <summary>
    /// Represents the current state of the match
    /// </summary>
    public enum MatchState
    {
        /// <summary>
        /// Match cannot be played until further bracket progress is made
        /// </summary>
        Pending,
        /// <summary>
        /// Match is currently available to be played
        /// </summary>
        Open,
        /// <summary>
        /// Match is over
        /// </summary>
        Complete
    }

    /// <summary>
    /// JsonConverter used to convert enum TournamentType
    /// </summary>
    public class TournamentTypeConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(string);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {

            switch ((string)reader.Value)
            {
                case "single elimination":
                    return TournamentType.SingleElimination;
                case "double elimination":
                    return TournamentType.DoubleElimination;
                case "round robin":
                    return TournamentType.RoundRobin;
                case "swiss":
                    return TournamentType.Swiss;
            }

            return null;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            switch ((TournamentType)value)
            {
                case TournamentType.SingleElimination:
                    writer.WriteValue("single elimination");
                    break;
                case TournamentType.DoubleElimination:
                    writer.WriteValue("double elimination");
                    break;
                case TournamentType.RoundRobin:
                    writer.WriteValue("round robin");
                    break;
                case TournamentType.Swiss:
                    writer.WriteValue("swiss");
                    break;
            }
        }
    }

    /// <summary>
    /// JsonConverter used to convert enum TournamentState
    /// </summary>
    public class TournamentStateConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(string);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            switch ((string)reader.Value)
            {
                case "pending":
                    return TournamentState.Pending;
                case "underway":
                    return TournamentState.Underway;
                case "awaiting_review":
                    return TournamentState.AwaitingReview;
                case "complete":
                    return TournamentState.Complete;
            }

            return null;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            switch ((TournamentState)value)
            {
                case TournamentState.Pending:
                    writer.WriteValue("pending");
                    break;
                case TournamentState.Underway:
                    writer.WriteValue("underway");
                    break;
                case TournamentState.AwaitingReview:
                    writer.WriteValue("awaiting_review");
                    break;
                case TournamentState.Complete:
                    writer.WriteValue("complete");
                    break;
            }
        }
    }

    /// <summary>
    /// JsonConverter used to convert enum RankSystem
    /// </summary>
    public class RankSystemConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(string);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            switch ((string)reader.Value)
            {
                case "match wins":
                    return RankSystem.MatchWins;
                case "game wins":
                    return RankSystem.GameWins;
                case "game win percentage":
                    return RankSystem.GameWinPercentage;
                case "points scored":
                    return RankSystem.PointsScored;
                case "points difference":
                    return RankSystem.PointsDifference;
                case "custom":
                    return RankSystem.Custom;
            }

            return null;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            switch ((RankSystem)value)
            {
                case RankSystem.MatchWins:
                    writer.WriteValue("match wins");
                    break;
                case RankSystem.GameWins:
                    writer.WriteValue("game wins");
                    break;
                case RankSystem.GameWinPercentage:
                    writer.WriteValue("game win percentage");
                    break;
                case RankSystem.PointsScored:
                    writer.WriteValue("points scored");
                    break;
                case RankSystem.PointsDifference:
                    writer.WriteValue("points difference");
                    break;
                case RankSystem.Custom:
                    writer.WriteValue("custom");
                    break;
            }
        }
    }

    /// <summary>
    /// JsonConverter used to convert enum GrandFinalsModifier
    /// </summary>
    public class GrandFinalsModifierConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(string);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.Value == null)
                return GrandFinalsModifier.TwoSets;

            switch ((string)reader.Value)
            {
                case "single match":
                    return GrandFinalsModifier.OneSet;
                case "skip":
                    return GrandFinalsModifier.None;
            }

            return null;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            switch ((GrandFinalsModifier)value)
            {
                case GrandFinalsModifier.TwoSets:
                    writer.WriteValue(string.Empty);
                    break;
                case GrandFinalsModifier.OneSet:
                    writer.WriteValue("single match");
                    break;
                case GrandFinalsModifier.None:
                    writer.WriteValue("skip");
                    break;
            }
        }
    }

    /// <summary>
    /// JsonConverter used to convert enum MatchState
    /// </summary>
    public class MatchStateConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(string);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            switch ((string)reader.Value)
            {
                case "pending":
                    return MatchState.Pending;
                case "open":
                    return MatchState.Open;
                case "complete":
                    return MatchState.Complete;
            }

            return null;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            switch ((MatchState)value)
            {
                case MatchState.Pending:
                    writer.WriteValue("pending");
                    break;
                case MatchState.Open:
                    writer.WriteValue("open");
                    break;
                case MatchState.Complete:
                    writer.WriteValue("complete");
                    break;
            }
        }
    }
}

namespace bracketAPI.Challonge.Enums.Filters
{
    /// <summary>
    /// Parameters used for Tournament Type filtering
    /// </summary>
    public enum TournamentType
    {
        /// <summary>
        /// All Tournaments
        /// </summary>
        All,
        /// <summary>
        /// Single Elimination tournaments
        /// </summary>
        SingleElimination,
        /// <summary>
        /// Double Elimination tournaments
        /// </summary>
        DoubleElimination,
        /// <summary>
        /// Round Robin tournaments
        /// </summary>
        RoundRobin,
        /// <summary>
        /// Swiss tournaments
        /// </summary>
        Swiss
    }

    /// <summary>
    /// Parameters used for Tournament State filtering
    /// </summary>
    public enum TournamentState
    {
        /// <summary>
        /// All Tournaments
        /// </summary>
        All,
        /// <summary>
        /// Pending tournaments
        /// </summary>
        Pending,
        /// <summary>
        /// Tournaments in progress
        /// </summary>
        InProgress,
        /// <summary>
        /// Completed tournaments
        /// </summary>
        Ended
    }

    /// <summary>
    /// Represents the current state of the match
    /// </summary>
    public enum MatchState
    {
        /// <summary>
        /// All matches
        /// </summary>
        All,
        /// <summary>
        /// Pending matches
        /// </summary>
        Pending,
        /// <summary>
        /// Available matches
        /// </summary>
        Open,
        /// <summary>
        /// Completed matches
        /// </summary>
        Complete
    }

    public static class Converters
    {
        public static string TournamentStateConverter(TournamentState tournamentState)
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

            return null;
        }

        public static string TournamentTypeConverter(TournamentType tournamentType)
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

            return null;
        }

        public static string MatchStateConverter(MatchState matchState)
        {
            switch (matchState)
            {
                case MatchState.All:
                    return "all";
                case MatchState.Pending:
                    return "pending";
                case MatchState.Open:
                    return "open";
                case MatchState.Complete:
                    return "complete";
            }

            return null;
        }
    }
}