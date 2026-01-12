using UnityEngine;

public class SpeedProgressionSystem : MonoBehaviour
{
    private void OnEnable()
    {
        GameConfig.Load();

        Debug.Log("GameConfig loaded: " + (GameConfig.Instance != null));

        EventBus.Subscribe("ScoreChanged", OnScoreChanged);
        EventBus.Subscribe("PlayerLethargic", OnPlayerLethargic);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe("ScoreChanged", OnScoreChanged);
        EventBus.Unsubscribe("PlayerLethargic", OnPlayerLethargic);
    }

    private void OnScoreChanged(object payload)
    {
        PlayerController player = payload as PlayerController;
        if (player == null) return;

        PlayerStats stats = player.stats;

        // does not progress while lethargic
        if (stats.speedState.Level < 0) return;

        int currentLevel = stats.speedState.Level;
        int[] thresholds = GameConfig.Instance.scoreThresholds;

        // already at max configured level
        if (currentLevel >= thresholds.Length) return;

        if (stats.score >= thresholds[currentLevel])
        {
            AdvanceSpeedState(stats);
        }
    }

    private void AdvanceSpeedState(PlayerStats stats)
    {
        int nextLevel = stats.speedState.Level + 1;

        stats.speedState = CreateSpeedState(nextLevel);

        EventBus.Publish("SpeedLevelChanged", stats.owner);

        Debug.Log(
            $"Player {stats.owner.playerID} advanced to Speed Level {nextLevel}"
        );
    }

    private void OnPlayerLethargic(object payload)
    {
        PlayerController player = payload as PlayerController;
        if (player == null) return;

        // could clear score but probably won't
        // player.stats.score = 0;
    }

    private PlayerSpeedState CreateSpeedState(int level)
    {
        switch (level)
        {
            case 0: return new SpeedLevel0();
            case 1: return new SpeedLevel1();
            case 2: return new SpeedLevel2();
            case 3: return new SpeedLevel3();
            case 4: return new SpeedLevel4();
            default: return new SpeedLevel4();
        }
    }
}
