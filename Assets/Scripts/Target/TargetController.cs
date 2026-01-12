using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class TargetController : MonoBehaviour
{
    public float moveSpeed = 4f;
    public Vector2 wanderBounds = new Vector2(8f, 4f);

    private Rigidbody2D rb;
    private TargetState currentState;

    [HideInInspector]
    public Vector2 wanderTarget;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        ChangeState(new TargetIdleState());
    }

    void Update()
    {
        currentState?.Update(this);
    }

    public void ChangeState(TargetState newState)
    {
        currentState?.Exit(this);
        currentState = newState;
        currentState.Enter(this);
    }

    public void MoveTowards(Vector2 target)
    {
        Vector2 newPos = Vector2.MoveTowards(
            rb.position,
            target,
            moveSpeed * Time.deltaTime
        );

        rb.MovePosition(newPos);
    }

    public Vector2 GetRandomWanderPoint()
    {
        return new Vector2(
            Random.Range(-wanderBounds.x, wanderBounds.x),
            Random.Range(-wanderBounds.y, wanderBounds.y)
        );
    }
}


