using TMPro;
using UnityEngine;


public class EnemySpawnInfoBox : MonoBehaviour
{
    public TextMeshProUGUI amountText;
    public TextMeshProUGUI nameText;


    public void SetInfo(int amount, string name)
    {
        amountText.text = amount.ToString();
        nameText.text = name;
    }
}
