using UnityEditor;
using UnityEngine;


namespace GTQuickView.Editor
{
    public static class QuickView
    {
        public static void Show(SerializedProperty property)
        {
            Show(property.objectReferenceValue);
        }

        public static void Show(Object objectToInspect)
        {
            if (objectToInspect == null)
            {
                return;
            }

            if (objectToInspect.GetType() == typeof(GameObject))
            {
                Debug.LogWarning("GameObjects are not supported");
                return;
            }

            var window = EditorWindow.GetWindow<QuickViewWindow>();
            window.SetReferenceObject(objectToInspect);
            window.Show();
        }

    }
}