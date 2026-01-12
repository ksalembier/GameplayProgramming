using UnityEngine;
using UnityEngine.UI;

public class ScoreBarUI : MonoBehaviour
{
    [SerializeField] private Slider scoreSlider;
    [SerializeField] private int playerID;

    private void Awake()
    {
        if (scoreSlider == null)
            scoreSlider = GetComponent<Slider>();

        scoreSlider.minValue = 0f;
        scoreSlider.maxValue = 1f;
        scoreSlider.value = 0f;
    }

    private void OnEnable()
    {
        EventBus.Subscribe("ScoreProgressUpdated", OnScoreProgressUpdated);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe("ScoreProgressUpdated", OnScoreProgressUpdated);
    }

    private void OnScoreProgressUpdated(object payload)
    {
        if (payload is not ScoreProgressData data) return;
        if (data.player.playerID != playerID) return;

        scoreSlider.value = data.normalizedProgress;
    }
}

