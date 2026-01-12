using UnityEngine;

public class ProximityScoringSystem : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TargetController target;

    [Header("Distance Tiers")]
    [SerializeField] private float closeRadius = .5f; // 1.5f;
    [SerializeField] private float mediumRadius = 1f; // 3f;
    [SerializeField] private float farRadius = 2f; // 5f;

    [Header("Points Per Second")]
    [SerializeField] private int closePoints = 5;
    [SerializeField] private int mediumPoints = 2;
    [SerializeField] private int farPoints = 1;

    void Update()
    {
        if (target == null) return;

        foreach (var player in PlayerRegistry.Players)
        {
            if (player == null) continue;

            if (player.stats.speedState.Level < 0) continue;

            float distance = Vector2.Distance(
                player.transform.position,
                target.transform.position
            );

            int points = GetPointsForDistance(distance);

            if (points > 0)
            {
                AddScore(player, points * Time.deltaTime);
            }
        }
    }

    int GetPointsForDistance(float distance)
    {
        if (distance <= closeRadius) return closePoints;
        if (distance <= mediumRadius) return mediumPoints;
        if (distance <= farRadius) return farPoints;
        return 0;
    }

    void AddScore(PlayerController player, float amount)
    {
        player.stats.score += amount;
        EventBus.Publish("ScoreChanged", player);
    }
}
