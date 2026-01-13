using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneFlowManager
{
    public static void LoadStartScene()
    {
        SceneManager.LoadScene("Start");
    }

    public static void LoadGameScene()
    {
        SceneManager.LoadScene("Core");
    }

    public static void LoadEndScene()
    {
        SceneManager.LoadScene("End");
    }
}

