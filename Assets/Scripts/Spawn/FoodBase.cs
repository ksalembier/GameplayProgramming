using UnityEngine;

public abstract class FoodBase : MonoBehaviour
{
    [Header("Lifetime")]
    [SerializeField] protected float lifetime = 8f;

    protected virtual void Start()
    {
        if (lifetime > 0f)
        {
            Destroy(gameObject, lifetime);
        }
    }

    protected abstract void OnCollected(PlayerController player);

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController player = other.GetComponent<PlayerController>();
        if (player == null) return;

        OnCollected(player);
        EventBus.Publish("FoodCollected", player);
        Destroy(gameObject);
    }
}
