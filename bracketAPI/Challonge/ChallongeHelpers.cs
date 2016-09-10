using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bracketAPI.Challonge
{
    public static class ChallongeHelpers
    {
        /// <summary>
        /// Converts enum TournamentState to proper format for parameter usage
        /// </summary>
        public static string ParseEnum(TournamentState tournamentState)
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

            //Suppress error CS0161
            return string.Empty;
        }

        /// <summary>
        /// Converts enum TournamentType to proper format for parameter usage
        /// </summary>
        public static string ParseEnum(TournamentType tournamentType)
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

            //Suppress error CS0161
            return string.Empty;
        }

        /// <summary>
        /// Converts string passed from API to enum TournamentState
        /// </summary>
        public static TournamentState ParseTournamentState(string tournamentState)
        {
            switch (tournamentState)
            {
                case "pending":
                    return TournamentState.Pending;
                case "underway":
                    return TournamentState.InProgress;
                case "complete":
                    return TournamentState.Ended;
            }

            //Suppress error CS0161
            return 0;
        }

        /// <summary>
        /// Converts string passed from API to enum TournamentType
        /// </summary>
        public static TournamentType ParseTournamentType(string tournamentType)
        {
            switch (tournamentType)
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

            //Suppress error CS0161
            return 0;
        }

        /// <summary>
        /// Converts string passed from API to enum MatchState
        /// </summary>
        public static MatchState ParseMatchState(string matchState)
        {
            switch (matchState)
            {
                case "open":
                    return MatchState.Open;
                case "pending":
                    return MatchState.Pending;
                case "complete":
                    return MatchState.Complete;
            }

            //Suppress error CS0161
            return 0;
        }

    }
}