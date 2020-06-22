using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class MultipleButton : MonoBehaviour
{
    public Button[] controlButtons;
    private int currentButttonIndex;

    private void Start()
    {
        foreach (var button in controlButtons)
        {
            button.onClick.AddListener(SwitchButton);
            button.gameObject.SetActive(false);
        }
        if (controlButtons.Length != 0)
        {
            controlButtons[0].gameObject.SetActive(true);
        }
    }

    [ContextMenu("Switch Buttons")]
    private void SwitchButton()
    {
        controlButtons[currentButttonIndex].gameObject.SetActive(false);
        currentButttonIndex += 1;
        currentButttonIndex = currentButttonIndex % controlButtons.Length;
        controlButtons[currentButttonIndex].gameObject.SetActive(true);
    }
}
