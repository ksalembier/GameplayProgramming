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
                $"Player {result.playerID + 1}\n" +
                $"Score: {Mathf.FloorToInt(result.score)}\n\n";
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

