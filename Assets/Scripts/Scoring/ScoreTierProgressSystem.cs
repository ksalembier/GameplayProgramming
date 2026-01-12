using UnityEngine;

public class ScoreTierProgressSystem : MonoBehaviour
{
    private void OnEnable()
    {
        GameConfig.Load();
        EventBus.Subscribe("ScoreChanged", OnScoreChanged);
        EventBus.Subscribe("SpeedLevelChanged", OnSpeedLevelChanged);
        EventBus.Subscribe("PlayerLethargic", OnPlayerLethargic);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe("ScoreChanged", OnScoreChanged);
        EventBus.Unsubscribe("SpeedLevelChanged", OnSpeedLevelChanged);
        EventBus.Unsubscribe("PlayerLethargic", OnPlayerLethargic);
    }

    private void OnScoreChanged(object payload)
    {
        PlayerController player = payload as PlayerController;
        if (player == null) return;

        PublishScoreProgress(player);
    }

    private void OnSpeedLevelChanged(object payload)
    {
        PlayerController player = payload as PlayerController;
        if (player == null) return;

        // force UI reset on level up
        PublishScoreProgress(player);
    }

    private void OnPlayerLethargic(object payload)
    {
        PlayerController player = payload as PlayerController;
        if (player == null) return;

        EventBus.Publish("ScoreProgressUpdated", new ScoreProgressData(player, 0f));
    }

    private void PublishScoreProgress(PlayerController player)
    {
        PlayerStats stats = player.stats;

        if (stats.speedState.Level < 0)
        {
            EventBus.Publish("ScoreProgressUpdated",
                new ScoreProgressData(player, 0f));
            return;
        }

        int level = stats.speedState.Level;
        int[] thresholds = GameConfig.Instance.scoreThresholds;

        if (level >= thresholds.Length)
        {
            EventBus.Publish("ScoreProgressUpdated",
                new ScoreProgressData(player, 1f));
            return;
        }

        float tierMin = (level == 0) ? 0f : thresholds[level - 1];
        float tierMax = thresholds[level];

        float progress = Mathf.Clamp01(
            (stats.score - tierMin) / (tierMax - tierMin)
        );

        EventBus.Publish("ScoreProgressUpdated",
            new ScoreProgressData(player, progress));
    }
}
