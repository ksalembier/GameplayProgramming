using UnityEngine;

public class TargetWanderState : TargetState
{
    private float arriveThreshold = 0.1f;

    public override void Enter(TargetController target)
    {
        target.wanderTarget = target.GetRandomWanderPoint();
    }

    public override void Update(TargetController target)
    {
        target.MoveTowards(target.wanderTarget);

        if (Vector2.Distance(target.transform.position, target.wanderTarget) < arriveThreshold)
        {
            target.ChangeState(new TargetIdleState());
        }
    }

    public override void Exit(TargetController target) { }
}
