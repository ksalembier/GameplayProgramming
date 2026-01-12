using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    public int playerID;
    public PlayerStats stats = new PlayerStats();
    public PlayerStats Stats => stats;

    public CommandQueue commandQueue = new CommandQueue();
    public IInputProvider inputProvider;

    private Rigidbody2D rb;
    public float baseSpeed;

    private Vector2 pendingMove;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        GameConfig.Load();
        baseSpeed = GameConfig.Instance.baseSpeed;

        stats.owner = this;
        inputProvider = GetComponent<IInputProvider>();
    }

    void Update()
    {
        var command = inputProvider.GetCommand();
        if (command != null)
        {
            commandQueue.Enqueue(command);
        }

        var next = commandQueue.Dequeue();
        next?.Execute(this);

        DrainEnergy();
    }

    public void Move(Vector2 direction)
    {
        float speed = baseSpeed * stats.speedState.SpeedMultiplier;

        Debug.Log(
            $"Player {playerID} | baseSpeed={baseSpeed} | multiplier={stats.speedState.SpeedMultiplier} | finalSpeed={speed}"
        );

        rb.MovePosition(rb.position + direction * speed * Time.deltaTime);
    }

    void DrainEnergy()
    {
        // stats.energy -= Time.deltaTime;
        stats.energy -= GameConfig.Instance.energyDrainRate * Time.deltaTime;

        if (stats.energy <= 0 && stats.speedState.Level != -1)
        {
            stats.speedState = new SpeedLevelMinus1();
            EventBus.Publish("PlayerLethargic", this);
        }

        EventBus.Publish("EnergyChanged", this);
    }

    void OnEnable()
    {
        PlayerRegistry.Register(this);
    }

    void OnDisable()
    {
        PlayerRegistry.Unregister(this);
    }
}
