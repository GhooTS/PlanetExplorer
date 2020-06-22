using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;



public class WaveDataEditorWindow : EditorWindow
{
    public SerializedProperty waveDataProperty;
    public WaveData waveData;
    public int waveIndex;
    private List<bool> foldout = new List<bool>();
    private GUIContent[] spawnNames = new GUIContent[0];
    private int[] spawnIndex = new int[0];
    private bool loopRelation = false;
    private Vector2 scrollVector;

    private void Awake()
    {
        titleContent = new GUIContent("Wave Data Editor");
        LoadPreferWindowPosition();
    }




    private void OnGUI()
    {
        if (waveDataProperty != null && waveDataProperty.serializedObject != null)
        {
            scrollVector = EditorGUILayout.BeginScrollView(scrollVector);

            waveDataProperty.serializedObject.Update();

            UpdateSpawnDataRelation();

            var waveName = waveDataProperty.FindPropertyRelative("waveName");
            var spawnData = waveDataProperty.FindPropertyRelative("spawnDatas");

            EditorGUILayout.PropertyField(waveName);

            if (GUILayout.Button("Add spawn"))
            {
                var newElementIndex = spawnData.arraySize;
                spawnData.InsertArrayElementAtIndex(newElementIndex);
                spawnData.GetArrayElementAtIndex(newElementIndex).FindPropertyRelative("name").stringValue = $"Spawn Data {newElementIndex}";
            }

            DrawSpawnData(spawnData);

            waveDataProperty.serializedObject.ApplyModifiedProperties();
            DrawSummary();
            EditorGUILayout.EndScrollView();
        }
    }

    private void UpdateSpawnDataRelation()
    {
        if (loopRelation == false)
        {
            waveData.ManageRelation();
        }
        loopRelation = false;
    }

    private void DrawSummary()
    {
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Summary", EditorStyles.boldLabel);
        CustomeGUIUtility.DrawReadonlyProperty("Wave Start Time", $"{waveData.GetWaveSpawnStartTime()} s");
        CustomeGUIUtility.DrawReadonlyProperty("Wave Spawn Duration", $"{waveData.GetSpawnEndTime()} s");
        CustomeGUIUtility.DrawReadonlyProperty("Enemies in wave", $"{waveData.GetEnemiesCount()}");

        var enemies = waveData.GetDiffrenceEnemies();
        if (enemies != null)
        {
            foreach (var enemy in enemies)
            {
                CustomeGUIUtility.DrawReadonlyProperty(enemy.name, waveData.GetEnemiesCount(enemy).ToString());
            }
        }

    }

    private void DrawSpawnData(SerializedProperty spawnData)
    {
        for (int i = 0; i < spawnData.arraySize; i++)
        {
            if (foldout.Count <= i)
            {
                foldout.Add(false);
            }

            var name = spawnData.GetArrayElementAtIndex(i).FindPropertyRelative("name").stringValue;

            foldout[i] = EditorGUILayout.BeginFoldoutHeaderGroup(foldout[i], name, null,
                (position) =>
                {
                    var menu = new GenericMenu();

                    menu.AddItem(new GUIContent("Remove"), false, (index) =>
                    {
                        spawnData.serializedObject.Update();
                        spawnData.DeleteArrayElementAtIndex((int)index);
                        spawnData.serializedObject.ApplyModifiedProperties();
                    }, i);

                    menu.DropDown(position);
                });

            if (foldout[i])
            {
                DrawSpawnDataProperties(spawnData, i);
            }
            EditorGUILayout.EndFoldoutHeaderGroup();
        }

        //Remove redundant foldout
        if (spawnData.arraySize != foldout.Count)
        {
            foldout.RemoveRange(spawnData.arraySize, foldout.Count - spawnData.arraySize);
        }
    }

    private void DrawSpawnDataProperties(SerializedProperty spawnData, int i)
    {
        EditorGUI.BeginChangeCheck();
        //Get all properties;
        var currentSpawnData = spawnData.GetArrayElementAtIndex(i);
        var spawnName = currentSpawnData.FindPropertyRelative("name");
        var prefab = currentSpawnData.FindPropertyRelative("prefab");
        var spawnPoint = currentSpawnData.FindPropertyRelative("spawnPoint");
        var spawnAmount = currentSpawnData.FindPropertyRelative("amount");
        var spawnInterval = currentSpawnData.FindPropertyRelative("interval");
        var spawnDelay = currentSpawnData.FindPropertyRelative("delay");
        var spawnRelation = currentSpawnData.FindPropertyRelative("relation");
        var spawnRelationIndex = currentSpawnData.FindPropertyRelative("relationIndex");
        var spawnRelationDelay = currentSpawnData.FindPropertyRelative("relationDelay");


        //Draw properties
        EditorGUILayout.PropertyField(prefab);
        EditorGUILayout.PropertyField(spawnPoint);
        EditorGUILayout.PropertyField(spawnAmount);
        EditorGUILayout.PropertyField(spawnInterval);
        DrawSpawnDelayProperty(i, spawnName.stringValue, spawnDelay, spawnRelationIndex, spawnRelationDelay);
        DrawSpawnRelationProperty(spawnData, i, spawnRelation, spawnRelationIndex);

        if (EditorGUI.EndChangeCheck())
        {
            var prefabName = prefab.objectReferenceValue != null ? prefab.objectReferenceValue.name : "NULL";
            var spawnPointName = spawnPoint.objectReferenceValue != null ? spawnPoint.objectReferenceValue.name : "NULL";

            spawnName.stringValue = $"Spawn {spawnAmount.intValue} {prefabName} every {spawnInterval.floatValue} s with {spawnDelay.floatValue} s delay from {spawnPointName}";
        }
    }

    private void DrawSpawnRelationProperty(SerializedProperty spawnData, int i, SerializedProperty spawnRelation, SerializedProperty spawnRelationIndex)
    {
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.PropertyField(spawnRelation);

        //Get all spawn data related to this spawn data
        var excludedIndex = FindIndexRelatedToSpawnData(spawnData, i).ToArray();

        CustomeGUIUtility.GetNumbersContent(ref spawnNames, spawnData, "name", excludedIndex);
        CustomeGUIUtility.GetValues(ref spawnIndex, spawnData.arraySize, excludedIndex);

        if (spawnRelation.enumValueIndex != 0)
        {
            spawnRelationIndex.intValue = EditorGUILayout.IntPopup(spawnRelationIndex.intValue, spawnNames, spawnIndex);
        }
        else
        {
            spawnRelationIndex.intValue = -1;
        }
        EditorGUILayout.EndHorizontal();
    }

    private void DrawSpawnDelayProperty(int i, string spawnName, SerializedProperty spawnDelay, SerializedProperty spawnRelationIndex, SerializedProperty spawnRelationDelay)
    {
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.PropertyField(spawnDelay);

        //Drwa relation delay if spawn data has relation
        if (spawnRelationIndex.intValue != -1)
        {
            EditorGUILayout.LabelField($" + {spawnRelationDelay.floatValue}");
        }
        EditorGUILayout.EndHorizontal();

        //Display appropriate help box if created loop realtion 
        var loopRelatedSpawnDataIndex = IsLoopRelation(i);
        if (loopRelatedSpawnDataIndex != -1)
        {
            loopRelation = true;
            var loopRelatedSpawnDataName = waveData.spawnDatas[loopRelatedSpawnDataIndex].name;
            EditorGUILayout.HelpBox($"Loop realtion betweeen {spawnName} and {loopRelatedSpawnDataName}", MessageType.Error);
        }
    }

    private List<int> FindIndexRelatedToSpawnData(SerializedProperty spawnData, int index)
    {
        List<int> output = new List<int>();


        for (int i = 0; i < spawnData.arraySize; i++)
        {
            var relationIndex = spawnData.GetArrayElementAtIndex(i).FindPropertyRelative("relationIndex").intValue;

            if (i == index || relationIndex == index)
            {
                output.Add(i);
            }
        }
        return output;
    }

    private int IsLoopRelation(int index)
    {
        List<int> visitedIndex = new List<int>();

        var relation = waveData.spawnDatas[index].relation;
        var relationIndex = waveData.spawnDatas[index].relationIndex;
        visitedIndex.Add(index);

        while (relationIndex != -1 && relation != SpawnDataRelation.none)
        {
            if (visitedIndex.Contains(relationIndex) != false)
            {
                return visitedIndex.Last();
            }
            visitedIndex.Add(relationIndex);
            relation = waveData.spawnDatas[relationIndex].relation;
            relationIndex = waveData.spawnDatas[relationIndex].relationIndex;
        }

        return -1;
    }

    private void OnSelectionChange()
    {
        Close();
    }

    private void SavePreferWindowPosition()
    {
        EditorPrefs.SetFloat("WaveDataEditorXMin", position.xMin);
        EditorPrefs.SetFloat("WaveDataEditorXMax", position.xMax);
        EditorPrefs.SetFloat("WaveDataEditorYMin", position.yMin);
        EditorPrefs.SetFloat("WaveDataEditorYMax", position.yMax);
    }

    private void LoadPreferWindowPosition()
    {
        if (EditorPrefs.HasKey("WaveDataEditorXMin") == false)
        {
            return;
        }

        Rect savedPosition = new Rect();

        savedPosition.xMin = EditorPrefs.GetFloat("WaveDataEditorXMin");
        savedPosition.xMax = EditorPrefs.GetFloat("WaveDataEditorXMax");
        savedPosition.yMin = EditorPrefs.GetFloat("WaveDataEditorYMin");
        savedPosition.yMax = EditorPrefs.GetFloat("WaveDataEditorYMax");

        if (savedPosition.width + savedPosition.height != 0)
        {
            position = savedPosition;
        }
    }

    private void OnDisable()
    {
        SavePreferWindowPosition();
    }
}
