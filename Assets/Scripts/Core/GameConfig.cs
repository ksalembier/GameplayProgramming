using UnityEngine;

[CreateAssetMenu(fileName = "GameConfig", menuName = "Scriptable Objects/GameConfig")]
public class GameConfig : ScriptableObject
{
    public static GameConfig Instance;

    public float baseSpeed = 3f;
    public float energyDrainRate = 1f;
    public int[] scoreThresholds = new int[] { 50, 150, 350, 700 };
    public float sessionDuration = 60f;

    public static void Load()
    {
        if (Instance == null)
        {
            Instance = Resources.Load<GameConfig>("GameConfig");
            if (Instance == null)
            {
                Debug.LogError("GameConfig not found");
            }
        }
    }
}

