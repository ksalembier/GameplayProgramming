using UnityEngine;

public class MoveUpCommand : IPlayerCommand
{
    public void Execute(PlayerController player)
    {
        player.Move(Vector2.up);
    }
}

public class MoveDownCommand : IPlayerCommand
{
    public void Execute(PlayerController player)
    {
        player.Move(Vector2.down);
    }
}

public class MoveLeftCommand : IPlayerCommand
{
    public void Execute(PlayerController player)
    {
        player.Move(Vector2.left);
    }
}

public class MoveRightCommand : IPlayerCommand
{
    public void Execute(PlayerController player)
    {
        player.Move(Vector2.right);
    }
}

public class NeutralCommand : IPlayerCommand
{
    public void Execute(PlayerController player)
    {
        // intentionally does nothing
    }
}