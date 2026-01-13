using UnityEngine;

public class SessionManager : MonoBehaviour
{
    private float timeRemaining;
    private bool sessionActive;

    private void Start()
    {
        GameConfig.Load();
        timeRemaining = GameConfig.Instance.sessionDuration;
        sessionActive = true;
    }

    private void Update()
    {
        if (!sessionActive) return;

        timeRemaining -= Time.deltaTime;

        EventBus.Publish("SessionTimeUpdated", timeRemaining);

        if (timeRemaining <= 0f)
        {
            EndSession();
        }
    }

    private void EndSession()
    {
        sessionActive = false;

        // Capture final results
        EndRoundData.Capture();

        SceneFlowManager.LoadEndScene();
    }
}

