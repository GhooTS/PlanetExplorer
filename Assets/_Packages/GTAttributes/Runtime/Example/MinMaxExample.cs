using UnityEngine;

namespace GTAttribute.Examples
{
    public class MinMaxExample : MonoBehaviour
    {
        [GT_MinMaxRange("Some Value Name", 0, 100)]
        public float min;
        [HideInInspector]
        public float max;
    }
}