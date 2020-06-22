using UnityEngine;
public class TestingCommand : Command
{
    private string name;

    public TestingCommand()
    {
        name = $"command {Time.time.GetHashCode()}";
    }

    public override void Execute()
    {
        Debug.Log("Redo: " + name);
    }

    public override void UnExecute()
    {
        Debug.Log("Undo: " + name);
    }
}
