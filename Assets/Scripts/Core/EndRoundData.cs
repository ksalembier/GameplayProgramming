using System.Collections.Generic;

public static class EndRoundData
{
    public static List<PlayerResult> Results = new();

    public static void Capture()
    {
        Results.Clear();

        foreach (var player in PlayerRegistry.Players)
        {
            Results.Add(new PlayerResult
            {
                playerID = player.playerID,
                score = player.stats.score,
                speedLevel = player.stats.speedState.Level
            });
        }
    }
}

public struct PlayerResult
{
    public int playerID;
    public float score;
    public int speedLevel;
}
