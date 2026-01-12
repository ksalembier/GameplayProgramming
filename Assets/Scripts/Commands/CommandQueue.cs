using System.Collections.Generic;

public class CommandQueue
{
    private Queue<IPlayerCommand> queue = new Queue<IPlayerCommand>();

    public void Enqueue(IPlayerCommand command)
    {
        queue.Enqueue(command);
    }

    public IPlayerCommand Dequeue()
    {
        return queue.Count > 0 ? queue.Dequeue() : null;
    }
}
