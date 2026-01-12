using UnityEngine;

public class FoodSpawnManager : MonoBehaviour
{
    public SimpleFoodFactory factory;
    [SerializeField] private Vector2 spawnBounds = new Vector2(8f, 4f);

    void Start()
    {
        InvokeRepeating(nameof(SpawnFood), 2f, 6f);
    }

    void SpawnFood()
    {
        // Vector2 pos = Random.insideUnitCircle * 4f;

        Vector2 pos = new Vector2(
            Random.Range(-spawnBounds.x, spawnBounds.x),
            Random.Range(-spawnBounds.y, spawnBounds.y)
        );

        factory.CreateFood(pos);
    }
}
