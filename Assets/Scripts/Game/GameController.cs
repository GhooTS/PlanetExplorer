using GTVariable;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu]
public class GameResultData : ScriptableObject
{
    public bool gameResult;
    public IntVariable enemiesKilld;
    public IntVariable WaveClear;
    public IntVariable CompliationTime;
}

public class GameController : MonoBehaviour
{
    [SerializeField]
    [GTAttribute.GT_QuickView]
    private BoolVariable gameResultData;
    [SerializeField]
    private BoolReference gameResult;
    [SerializeField]
    private FloatReference value;
    public UnityEvent gameStarted;
    public UnityEvent gameEnded;


    private void Start()
    {
        StartGame();
    }


    public void StartGame()
    {
        gameStarted?.Invoke();
    }

    public void EndGame(bool result)
    {
        gameResultData.SetValue(result);
        gameEnded?.Invoke();
    }
}
