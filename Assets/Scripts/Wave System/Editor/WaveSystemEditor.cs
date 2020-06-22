using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(WaveSystem))]
public class WaveSystemEditor : Editor
{
    int selected = 0;
    WaveSystem waveSystem;
    WaveDataEditorWindow waveDataEditorWindow;
    [SerializeField] Texture editIcon;
    [SerializeField] Texture moveUpIcon;
    [SerializeField] Texture moveDownIcon;
    [SerializeField] Texture deleteIcon;

    private void OnEnable()
    {
        waveSystem = serializedObject.targetObject as WaveSystem;
    }

    public override void OnInspectorGUI()
    {
        selected = GUILayout.SelectionGrid(selected, new string[] { "Waves", "Data", "Event", "Options" }, 4, EditorStyles.miniButton);
        serializedObject.Update();
        switch (selected)
        {
            case 0:
                DrawWavesSection();
                break;
            case 1:
                DrawProperties("currentWaveNumber", "aliveEnemies");
                break;
            case 2:
                DrawProperties("waveRushed", "allWavesCleared");
                break;
            case 3:
                DrawProperties("autoWaveStart", "waveDuration");
                break;
        }
        serializedObject.ApplyModifiedProperties();

    }


    private void DrawProperties(string fromPropertyName, string toPropertyName)
    {
        var property = serializedObject.FindProperty(fromPropertyName);
        EditorGUILayout.PropertyField(property);
        while (property.NextVisible(false))
        {
            EditorGUILayout.PropertyField(property);

            if (property.name == toPropertyName)
            {
                break;
            }
        }
    }

    private void DrawWavesSection()
    {
        var waves = serializedObject.FindProperty("waves");

        if (GUILayout.Button("Add Wave"))
        {
            var waveIndex = waves.arraySize;
            waves.InsertArrayElementAtIndex(waveIndex);
            waves.GetArrayElementAtIndex(waveIndex).FindPropertyRelative("waveName").stringValue = $"Wave {(waveIndex + 1)}";
        }

        DrwaWaves(waves);
        DrawWavesSummary();
    }

    private void DrawWavesSummary()
    {
        EditorGUILayout.LabelField("Summary", EditorStyles.boldLabel);
        CustomeGUIUtility.DrawReadonlyProperty("All waves spawn duration", $"{waveSystem.GetAllWavesDuration()} s");
        CustomeGUIUtility.DrawReadonlyProperty("Enemies count in all waves", $"{waveSystem.GetEnemiesCountInAllWaves()}");
        if (EditorApplication.isPlaying)
        {
            Repaint();
            EditorGUILayout.LabelField("Current Wave Stats", EditorStyles.boldLabel);
            var progressBarRect = GUILayoutUtility.GetRect(EditorGUIUtility.currentViewWidth, EditorGUIUtility.singleLineHeight);
            var enemiesLeft = waveSystem.AliveEnemies;
            var enemiesInWave = waveSystem.EnemiesInCurrentWave;
            CustomeGUIUtility.DrawReadonlyProperty("Current wave", $"{waveSystem.CurrentWaveNumber} / {waveSystem.MaxWaves}");
            var nextWaveTime = Mathf.Round(waveSystem.NextWaveStartTime);
            CustomeGUIUtility.DrawReadonlyProperty("Time to next wave", $"{nextWaveTime} s");
            EditorGUI.ProgressBar(progressBarRect, enemiesInWave == 0 ? 0 : enemiesLeft / enemiesInWave, $"enemies {enemiesLeft} / {enemiesInWave}");
        }
    }

    private void DrwaWaves(SerializedProperty waves)
    {
        for (int i = 0; i < waves.arraySize; i++)
        {
            EditorGUILayout.BeginHorizontal();
            var name = waves.GetArrayElementAtIndex(i).FindPropertyRelative("waveName").stringValue;
            EditorGUILayout.LabelField((i + 1).ToString(), GUILayout.MaxWidth(30));
            EditorGUILayout.LabelField(name);
            DrawWaveOptionsButtons(waves, i);

            EditorGUILayout.EndHorizontal();
        }
    }

    private void DrawWaveOptionsButtons(SerializedProperty waves, int i)
    {
        if (GUILayout.Button(editIcon, GUILayout.MaxWidth(20), GUILayout.MaxHeight(20)))
        {
            CrateWaveDataEditorWindow(waves, i);
        }
        if (GUILayout.Button(deleteIcon, GUILayout.MaxWidth(20), GUILayout.MaxHeight(20)))
        {
            if (waveDataEditorWindow != null && waveDataEditorWindow.waveIndex == i)
            {
                waveDataEditorWindow.waveDataProperty = null;
                waveDataEditorWindow.Close();
            }
            waves.DeleteArrayElementAtIndex(i);
        }

        var enable = GUI.enabled;

        GUI.enabled = i > 0;

        if (GUILayout.Button(moveUpIcon, GUILayout.MaxWidth(20), GUILayout.MaxHeight(20)))
        {
            waves.MoveArrayElement(i, i - 1);
        }

        GUI.enabled = i < waves.arraySize - 1;

        if (GUILayout.Button(moveDownIcon, GUILayout.MaxWidth(20), GUILayout.MaxHeight(20)))
        {
            waves.MoveArrayElement(i, i + 1);
        }

        GUI.enabled = enable;
    }

    private void CrateWaveDataEditorWindow(SerializedProperty waves, int i)
    {
        if (waveDataEditorWindow != null)
        {
            waveDataEditorWindow.Close();
        }

        waveDataEditorWindow = EditorWindow.CreateInstance<WaveDataEditorWindow>();
        waveDataEditorWindow.waveDataProperty = waves.GetArrayElementAtIndex(i);
        waveDataEditorWindow.waveData = waveSystem.waves[i];
        waveDataEditorWindow.waveIndex = i;
        waveDataEditorWindow.ShowModalUtility();
    }
}
