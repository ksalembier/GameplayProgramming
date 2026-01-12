using UnityEngine;

public class TargetIdleState : TargetState
{
    private float idleTime;
    private float timer;

    public override void Enter(TargetController target)
    {
        idleTime = Random.Range(1.5f, 3f);
        timer = 0f;
    }

    public override void Update(TargetController target)
    {
        timer += Time.deltaTime;

        if (timer >= idleTime)
        {
            target.ChangeState(new TargetWanderState());
        }
    }

    public override void Exit(TargetController target) { }
}
