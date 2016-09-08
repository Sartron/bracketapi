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
    /// Challonge API data request types
    /// </summary>
    public enum DataFormat
    {
        JSON,
        XML
    }
}
