using UnityEngine;

[CreateAssetMenu(menuName = "Debug/Event Debuger")]
public class EventDebuger : ScriptableObject
{
    public void PrintMessage(string message)
    {
        Debug.Log(message);
    }
}
