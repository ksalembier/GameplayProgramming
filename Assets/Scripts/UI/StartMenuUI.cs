using UnityEngine;

public class StartMenuUI : MonoBehaviour
{
    public void OnStartButtonPressed()
    {
        SceneFlowManager.LoadGameScene();
    }
}
