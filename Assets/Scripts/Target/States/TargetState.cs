using UnityEngine;

public abstract class TargetState
{
    public abstract void Enter(TargetController target);
    public abstract void Update(TargetController target);
    public abstract void Exit(TargetController target);
}
