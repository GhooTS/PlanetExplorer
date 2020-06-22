using GTVariable;
using TMPro;
using UnityEngine;

public class SellButton : MonoBehaviour
{
    Selection selection;
    public TextMeshProUGUI price;
    private Buildable buildable;
    [SerializeField]
    private FloatVariable playerResource;

    private void Awake()
    {
        selection = FindObjectOfType<Selection>();
        gameObject.SetActive(false);
    }

    public void Show()
    {
        if(selection.IsObjectSelected() && selection.Selected.TryGetComponent(out buildable))
        {
            gameObject.SetActive(true);
            price.text = ((int)(buildable.buildingData.price * buildable.buildingData.sellPrice)).ToString();
        }
    }

    public void Sell()
    {
        if(buildable != null)
        {
            playerResource.SetValueWithEvent(playerResource + (int)(buildable.buildingData.price * buildable.buildingData.sellPrice));
            Destroy(buildable.gameObject);
        }
    }
}
