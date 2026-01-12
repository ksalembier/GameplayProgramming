using UnityEngine;

public class SimpleFoodFactory : MonoBehaviour, IFoodFactory
{
    public GameObject foodPrefab;

    public GameObject CreateFood(Vector2 position)
    {
        return Instantiate(foodPrefab, position, Quaternion.identity);
    }
}
