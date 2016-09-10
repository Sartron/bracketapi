using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bracketAPI.Challonge
{
    /// <summary>
    /// Challonge tournament states
    /// </summary>
    public enum TournamentState
    {
        All,
        Pending,
        InProgress,
        Ended
    }

    /// <summary>
    /// Challonge tournament types
    /// </summary>
    public enum TournamentType
    {
        All,
        SingleElimination,
        DoubleElimination,
        RoundRobin,
        Swiss
    }

    /// <summary>
    /// Challonge match states
    /// </summary>
    public enum MatchState
    {
        All,
        Pending,
        Open,
        Complete
    }

    /// <summary>
    /// Challonge API data request types
    /// </summary>
    public enum DataFormat
    {
        JSON,
        XML
    }
}
