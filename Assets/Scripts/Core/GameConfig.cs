//using UnityEngine;

//[CreateAssetMenu(fileName = "GameConfig", menuName = "Scriptable Objects/GameConfig")]
//public class GameConfig : ScriptableObject
//{
//    public static GameConfig Instance;
//    public float baseSpeed = 3f;
//    public float energyDrainRate = 1f; // per second
//    public float foodRestoreAmount = 25f;
//    public int[] scoreThresholds = new int[] { 50, 150, 350, 700 };
//    public float sessionDuration = 120f; // seconds
//    public float foodSpawnInterval = 8f;
//    void OnEnable() { Instance = this; }
//}

using UnityEngine;

[CreateAssetMenu(fileName = "GameConfig", menuName = "Scriptable Objects/GameConfig")]
public class GameConfig : ScriptableObject
{
    public static GameConfig Instance;

    public float baseSpeed = 3f;
    public float energyDrainRate = 2f;
    public float foodRestoreAmount = 25f;
    public int[] scoreThresholds = new int[] { 50, 150, 350, 700 };
    public float sessionDuration = 120f;
    public float foodSpawnInterval = 8f;

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

