using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuyButton : MonoBehaviour
{
    public Buildable building;
    public TextMeshProUGUI price;
    public Image icon;
    private ToolManage toolManage;
    private BuildTool buildTool;


    private void Start()
    {
        toolManage = FindObjectOfType<ToolManage>();
        buildTool = toolManage.GetToolOfType<BuildTool>();
        price.text = Mathf.RoundToInt(building.buildingData.price).ToString();
        icon.sprite = building.buildingData.icon;
    }


    public void SetBuilding()
    {
        buildTool.Activate(building);
    }
}
