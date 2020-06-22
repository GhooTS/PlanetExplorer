using GTVariable;
using UnityEngine;

[CreateAssetMenu(menuName = "Building")]
public class BuildingData : ScriptableObject
{
    new public string name;
    public StringVariable type;
    public Sprite icon;
    [Min(0)]
    public float price;
    [Range(0, 1)]
    public float sellPrice;

}
