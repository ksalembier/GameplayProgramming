using UnityEngine;
using TMPro;

public class EndRoundSummaryUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI summaryText;

    private void Start()
    {
        BuildSummary();
    }

    private void BuildSummary()
    {
        summaryText.text = "Final Results\n\n";

        foreach (var result in EndRoundData.Results)
        {
            summaryText.text +=
                $"Player {result.playerID}\n" +
                $"Score: {Mathf.FloorToInt(result.score)}\n";
        }
    }

    public void OnRestartPressed()
    {
        SceneFlowManager.LoadGameScene();
    }

    public void OnReturnToMenuPressed()
    {
        SceneFlowManager.LoadStartScene();
    }
}

