using UnityEngine;

public class KeyboardInputProvider : MonoBehaviour, IInputProvider
{
    public int playerId; // 0 = WASD, 1 = Arrow Keys

    public IPlayerCommand GetCommand()
    {
        if (playerId == 0)
        {
            if (Input.GetKey(KeyCode.W)) return new MoveUpCommand();
            if (Input.GetKey(KeyCode.S)) return new MoveDownCommand();
            if (Input.GetKey(KeyCode.A)) return new MoveLeftCommand();
            if (Input.GetKey(KeyCode.D)) return new MoveRightCommand();
        }
        else if (playerId == 1)
        {
            if (Input.GetKey(KeyCode.UpArrow)) return new MoveUpCommand();
            if (Input.GetKey(KeyCode.DownArrow)) return new MoveDownCommand();
            if (Input.GetKey(KeyCode.LeftArrow)) return new MoveLeftCommand();
            if (Input.GetKey(KeyCode.RightArrow)) return new MoveRightCommand();
        }

        return new NeutralCommand();
    }
}
