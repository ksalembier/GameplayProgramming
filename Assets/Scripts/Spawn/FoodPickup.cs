using UnityEngine;

public class FoodPickup : MonoBehaviour
{
    [SerializeField] private float energyRestoreAmount = 25f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController player = other.GetComponent<PlayerController>();
        if (player == null) return;

        player.stats.RestoreEnergy(energyRestoreAmount);

        EventBus.Publish("FoodCollected", player);

        Destroy(gameObject);
    }
}