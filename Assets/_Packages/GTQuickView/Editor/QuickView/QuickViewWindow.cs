using UnityEditor;
using UnityEngine;

namespace GTQuickView.Editor
{
    public class QuickViewWindow : EditorWindow
    {
        private Object referenceObject;
        private UnityEditor.Editor editor;
        private Vector2 scrollVec;



        [MenuItem("Window/Qucik view", priority = 2950)]
        public static void Init()
        {
            GetWindow<QuickViewWindow>();
        }

        

        private void OnEnable()
        {
            titleContent = new GUIContent("Qucik View");
            autoRepaintOnSceneChange = true;
            DestroyEditor();
        }

        private void OnDisable()
        {
            DestroyEditor();
        }

        public void SetReferenceObject(Object refObject)
        {
            DestroyEditor();
            referenceObject = refObject;
            CreateEdtiorForRefObject();
        }

        private void DestroyEditor()
        {
            if (editor != null)
            {
                DestroyImmediate(editor);
            }
        }

        private void CreateEdtiorForRefObject()
        {
            editor = UnityEditor.Editor.CreateEditor(referenceObject);
        }

        private void OnGUI()
        {

            if (referenceObject == null)
            {
                DestroyEditor();
                return;
            }


            if (editor == null)
            {
                CreateEdtiorForRefObject();
            }

            scrollVec = EditorGUILayout.BeginScrollView(scrollVec);
            editor.DrawHeader();
            editor.OnInspectorGUI();
            EditorGUILayout.EndScrollView();
            EditorGUILayout.Space();
        }
    }
}