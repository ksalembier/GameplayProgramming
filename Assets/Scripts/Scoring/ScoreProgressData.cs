using UnityEngine;

public struct ScoreProgressData
{
    public PlayerController player;
    public float normalizedProgress;

    public ScoreProgressData(PlayerController player, float normalizedProgress)
    {
        this.player = player;
        this.normalizedProgress = normalizedProgress;
    }
}
