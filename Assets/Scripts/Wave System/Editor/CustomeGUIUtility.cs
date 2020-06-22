using System.Linq;
using UnityEditor;
using UnityEngine;

public class CustomeGUIUtility
{

    public static void DrawReadonlyProperty(string text, string value)
    {
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField(text);
        EditorGUILayout.LabelField(value);
        EditorGUILayout.EndHorizontal();
    }

    public static void GetNumbersContent(ref GUIContent[] content, SerializedProperty tab, string propertyName, params int[] excludeIndex)
    {
        var arraySize = excludeIndex.Length == 0 ? tab.arraySize : tab.arraySize - excludeIndex.Length;

        arraySize = arraySize < 0 ? 0 : arraySize;

        if (content.Length != arraySize)
        {
            content = new GUIContent[arraySize];
        }



        for (int index = 0, i = 0; i < tab.arraySize; i++)
        {
            if (excludeIndex.Contains(i))
            {
                continue;
            }
            content[index] = new GUIContent(tab.GetArrayElementAtIndex(i).FindPropertyRelative(propertyName).stringValue);
            index++;
        }
    }

    public static void GetValues(ref int[] content, int origineSize, params int[] excludeIndex)
    {
        var arraySize = excludeIndex.Length == 0 ? origineSize : origineSize - excludeIndex.Length;

        arraySize = arraySize < 0 ? 0 : arraySize;

        if (content.Length != arraySize)
        {
            content = new int[arraySize];
        }



        for (int index = 0, i = 0; i < origineSize; i++)
        {
            if (excludeIndex.Contains(i))
            {
                continue;
            }
            content[index] = i;
            index++;
        }
    }
}
