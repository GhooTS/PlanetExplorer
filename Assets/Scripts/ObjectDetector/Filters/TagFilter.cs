using GTAttribute;
using UnityEngine;

[System.Serializable]
public class TagFilter
{
    [GT_Tag]
    public string tag;

    public bool Pass(Transform objectToFilter)
    {
        return objectToFilter.CompareTag(tag);
    }
}
