using UnityEditor;
using UnityEngine;
using System.Linq;
using System.Threading.Tasks;

public class MissingComponentsUtilityWindow : EditorWindow
{
    private MissingComponentsUtility componentsUtility = new MissingComponentsUtility();
    private GUIStyle selectionGridStyle;
    private GUIStyle centerLabelStyle;
    private bool inScene;
    private bool inProject;
    private Vector2 scrollVec;
    private GUIContent[] names = new GUIContent[0];
    private GUIContent[] isPrefab = new GUIContent[0];
    private GUIContent[] missingComponentsCount = new GUIContent[0];
    private int selected = -1;


    [MenuItem("Window/Missing Components Utility")]
    public static void Init()
    {
        var window = GetWindow<MissingComponentsUtilityWindow>();
        window.titleContent = new GUIContent("Missing Components Utility");
    }

    private void OnEnable()
    {
        selectionGridStyle = null;
        names = new GUIContent[0];
        isPrefab = new GUIContent[0];
        missingComponentsCount = new GUIContent[0];
    }


    private void OnGUI()
    {
        if(selectionGridStyle == null)
        {
            selectionGridStyle = new GUIStyle(GUI.skin.button);

            selectionGridStyle.font = Font.CreateDynamicFontFromOSFont("Arial", 12);
            selectionGridStyle.fixedHeight = 20;
            selectionGridStyle.imagePosition = ImagePosition.ImageLeft;
            selectionGridStyle.alignment = TextAnchor.MiddleLeft;
            selectionGridStyle.border = new RectOffset(1, 1, 1, 1);
            selectionGridStyle.margin = new RectOffset(2, 2, 1, 1);
            selectionGridStyle.richText = true;

            centerLabelStyle = new GUIStyle(GUI.skin.label);
            centerLabelStyle.alignment = TextAnchor.MiddleCenter;
            centerLabelStyle.fixedHeight = 20;
            centerLabelStyle.border = new RectOffset(1, 1, 1, 1);
            centerLabelStyle.margin = new RectOffset(2, 2, 1, 1);
            centerLabelStyle.richText = true;
        }

        EditorGUILayout.BeginHorizontal();

        EditorGUILayout.BeginVertical(GUILayout.MaxWidth(70));
        if (GUILayout.Button("Refresh", GUILayout.MaxWidth(70)))
        {
            selected = -1;
            componentsUtility.Refresh(inScene, inProject);
            ConvertWrapperDataToGUIContent();
        }
        EditorGUILayout.LabelField("Serach in:");
        inScene = EditorGUILayout.ToggleLeft("scene", inScene);
        inProject = EditorGUILayout.ToggleLeft("projecte", inProject);
        EditorGUILayout.EndVertical();


        //if (GUILayout.Button("Remove all in scene", GUILayout.MaxWidth(300)))
        //{

        //}
        if (selected != -1 && selected < componentsUtility.gameObjects.Count)
        {
            EditorGUILayout.BeginVertical();
            EditorGUILayout.LabelField(new GUIContent("Name: "),names[selected]);
            EditorGUILayout.LabelField(new GUIContent("Localization: "),isPrefab[selected]);
            var missingComponentsGUI = new GUIContent(componentsUtility.gameObjects[selected].MissingComponents.ToString());
            var missingComponentsChildrenGUI = new GUIContent(componentsUtility.gameObjects[selected].MissingComponentsInChildrens.ToString());
            EditorGUILayout.LabelField(new GUIContent("Missing Components: "), missingComponentsGUI);
            EditorGUILayout.LabelField(new GUIContent("Missing Components In Children: "), missingComponentsChildrenGUI);
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Select"))
            {
                UnityEditor.Selection.activeGameObject = componentsUtility.gameObjects[selected].GameObject;
            }
            if (GUILayout.Button("Clear"))
            {
                GameObjectUtility.RemoveMonoBehavioursWithMissingScript(componentsUtility.gameObjects[selected].GameObject);
                componentsUtility.gameObjects.RemoveAt(selected);
                ConvertWrapperDataToGUIContent();
                if(selected >= componentsUtility.gameObjects.Count)
                {
                    selected = componentsUtility.gameObjects.Count - 1;
                }
            }
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.EndVertical();
        }
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.Space();


        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("NAMES", EditorStyles.centeredGreyMiniLabel);
        EditorGUILayout.LabelField("LOCALIZATION", EditorStyles.centeredGreyMiniLabel, GUILayout.MaxWidth(90));
        EditorGUILayout.LabelField(new GUIContent("#MC", "missing components count"), EditorStyles.centeredGreyMiniLabel, GUILayout.MaxWidth(50));
        EditorGUILayout.EndHorizontal();

        scrollVec = EditorGUILayout.BeginScrollView(scrollVec);

        

        EditorGUILayout.BeginHorizontal();
        selected = GUILayout.SelectionGrid(selected, names, 1,selectionGridStyle);
        selected = GUILayout.SelectionGrid(selected, isPrefab, 1, centerLabelStyle, GUILayout.MaxWidth(90));
        selected = GUILayout.SelectionGrid(selected, missingComponentsCount, 1, centerLabelStyle, GUILayout.MaxWidth(50));
        EditorGUILayout.EndHorizontal();


        EditorGUILayout.EndScrollView();
    }



    private void ConvertWrapperDataToGUIContent()
    {
        names = componentsUtility.gameObjects.ConvertAll(
                    (gameObjWrapper) => new GUIContent(EditorGUIUtility.ObjectContent(gameObjWrapper.GameObject, typeof(GameObject)))).ToArray();

        isPrefab = componentsUtility.gameObjects.ConvertAll(
                   (gameObjWrapper) => new GUIContent(gameObjWrapper.Localization.ToString())).ToArray();

        missingComponentsCount = componentsUtility.gameObjects.ConvertAll(
                                 (gameObjWrapper) => new GUIContent((gameObjWrapper.MissingComponents + gameObjWrapper.MissingComponentsInChildrens).ToString())).ToArray();
    }

    
}
