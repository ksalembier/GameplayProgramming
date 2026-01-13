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
    private float baseSpeed;

    private Vector2 pendingMove;

    private Camera mainCamera;
    private Vector2 minBounds;
    private Vector2 maxBounds;

    private float pushDistance = 0.5f;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        GameConfig.Load();
        baseSpeed = GameConfig.Instance.baseSpeed;

        stats.owner = this;
        inputProvider = GetComponent<IInputProvider>();

        mainCamera = Camera.main;

        Vector3 bottomLeft = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, 0));
        Vector3 topRight = mainCamera.ViewportToWorldPoint(new Vector3(1, 1, 0));

        minBounds = bottomLeft;
        maxBounds = topRight;
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

    private void FixedUpdate()
    {
        // prevents continued movement after bump
        rb.linearVelocity = Vector2.zero;
    }

    public void Move(Vector2 direction)
    {
        float speed = baseSpeed * stats.speedState.SpeedMultiplier;

        // enforcing bounds by clamping movement
        Vector2 targetPos = rb.position + direction * speed * Time.deltaTime;
        targetPos.x = Mathf.Clamp(targetPos.x, minBounds.x, maxBounds.x);
        targetPos.y = Mathf.Clamp(targetPos.y, minBounds.y, maxBounds.y);

        rb.MovePosition(targetPos);

        //Debug.Log(
        //$"Player {playerID} | baseSpeed={baseSpeed} | multiplier={stats.speedState.SpeedMultiplier} | finalSpeed={speed}");
    }

    void DrainEnergy()
    {
        // if lethargic, clamp energy at zero 
        if (stats.speedState.Level == -1)
        {
            if (stats.energy < 0f)
                stats.energy = 0f;

            EventBus.Publish("EnergyChanged", this);
            return;
        }

        // normal energy drain
        stats.energy -= GameConfig.Instance.energyDrainRate * Time.deltaTime;

        // entering lethargy state
        if (stats.energy <= 0f)
        {
            stats.energy = 0f;
            stats.speedState = new SpeedLevelMinus1();
            EventBus.Publish("PlayerLethargic", this);
        }

        Debug.Log("Energy drain rate: " + GameConfig.Instance.energyDrainRate);

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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerController other = collision.gameObject.GetComponent<PlayerController>();
        if (other == null) return;

        int myLevel = stats.speedState.Level;
        int otherLevel = other.stats.speedState.Level;

        // ignore lethargic interactions
        if (myLevel < 0 || otherLevel < 0)
            return;

        // if the other player is faster, this player gets pushed
        if (otherLevel > myLevel)
        {
            Vector2 pushDir = (transform.position - other.transform.position).normalized;

            rb.position += pushDir * pushDistance;
            rb.linearVelocity = Vector2.zero;
        }

        // if we have the same speed, we get mutual push
        if (otherLevel == myLevel)
        {
            Vector2 pushDir = (transform.position - other.transform.position).normalized;
            rb.position += pushDir * (pushDistance * 0.5f);
            rb.linearVelocity = Vector2.zero;
        }
    }
}