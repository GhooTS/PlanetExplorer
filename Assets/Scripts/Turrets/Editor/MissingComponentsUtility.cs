using UnityEditor;
using UnityEngine;
using System.Collections.Generic;


public class GameObjectWrapper
{
    public GameObject GameObject { get; set; }
    public MissingComponentsUtility.Localization Localization { get; set; }
    public int MissingComponents { get; set; }
    public int MissingComponentsInChildrens { get; set; }
}


public class MissingComponentsUtility
{
    public enum Localization
    {
        project,scene
    }

    public readonly List<GameObjectWrapper> gameObjects = new List<GameObjectWrapper>();

    private int missingGameObjectInSceneCount = 0;


    

    public void Refresh(bool scene = true,bool project = true)
    {
        gameObjects.Clear();
        if (scene) FindMissingComponentsInScene();
        if (project) FindMissingComponentsInProject();
    }

    public void Update()
    {
        for (int i = gameObjects.Count - 1; i >= 0; i--)
        {
            if (gameObjects[i] == null)
            {
                gameObjects.RemoveAt(i);

                if (i < missingGameObjectInSceneCount) missingGameObjectInSceneCount--;
            }
        }
    }


    //public void RemoveAllMissingComponentsFromScene()
    //{
    //    foreach (var gameObject in gameObjects)
    //    {
    //        GameObjectUtility.RemoveMonoBehavioursWithMissingScript(gameObject);
    //    }

    //    Clear();
    //}


    private void FindMissingComponentsInProject()
    {
        var prefabGUIDs = AssetDatabase.FindAssets($"t:{nameof(GameObject)}");

        //prefabPaths.Clear();
        //numberOfMissingScriptsPrefabs.Clear();
        foreach (var prefabGUID in prefabGUIDs)
        {
            //convert GUID to path
            var path = AssetDatabase.GUIDToAssetPath(prefabGUID);

            //Get prefab instance
            var prefabInstance = AssetDatabase.LoadAssetAtPath(path, typeof(GameObject)) as GameObject;

            //Get number of missing components
            var missingComponents = GameObjectUtility.GetMonoBehavioursWithMissingScriptCount(prefabInstance);
            var missingComponentsChildren = Mathf.Max(0,FindMissingComponentsRecursive(prefabInstance.transform) - missingComponents);


            //if prefab instance has any missing component, cache it's path and count of missing components
            if (missingComponents + missingComponentsChildren != 0)
            {
                //prefabPaths.Add(path);
                //numberOfMissingScriptsPrefabs.Add(numberOfMissingScirpts);
                gameObjects.Add(new GameObjectWrapper
                                    {
                                        GameObject = prefabInstance,
                                        MissingComponents = missingComponents,
                                        MissingComponentsInChildrens = missingComponentsChildren,
                                        Localization = Localization.project
                                    });
            }

            //Destroy prefab instance
            //GameObject.DestroyImmediate(prefabInstance);
        }
    }


    private int FindMissingComponentsRecursive(Transform parent)
    {
        int output = 0;

        foreach (Transform child in parent)
        {
            output += FindMissingComponentsRecursive(child);
        }

        output += GameObjectUtility.GetMonoBehavioursWithMissingScriptCount(parent.gameObject);

        return output;
    }

    private void FindMissingComponentsInScene()
    {
        foreach (var gameObject in GameObject.FindObjectsOfType<GameObject>())
        {
            int missingComponents = GameObjectUtility.GetMonoBehavioursWithMissingScriptCount(gameObject);
            if (missingComponents != 0)
            {
                gameObjects.Add(new GameObjectWrapper 
                                { 
                                    GameObject = gameObject, 
                                    MissingComponents = missingComponents, 
                                    MissingComponentsInChildrens = 0,
                                    Localization = Localization.scene
                                });
            }
        }
        missingGameObjectInSceneCount = gameObjects.Count;
    }
}
