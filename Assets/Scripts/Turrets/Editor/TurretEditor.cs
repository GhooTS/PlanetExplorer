using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Turret))]
public class TurretEditor : Editor
{

    Turret turret;
    GameObject targetGameObject;
    List<Editor> editors = new List<Editor>();


    private void OnEnable()
    {
        turret = target as Turret;
        targetGameObject = turret.gameObject;
    }

    public override void OnInspectorGUI()
    {

        DrawDefaultInspector();
        foreach (var editor in editors)
        {
            editor.DrawDefaultInspector();
        }
        if (GUILayout.Button("Add Component for projectile turret"))
        {
            AddComponentIfNotExist<SimpleTargetSelector>(targetGameObject);
            AddComponentIfNotExist<TurretShootSystem>(targetGameObject);
            AddComponentIfNotExist<TurretAmunition>(targetGameObject);
            AddComponentIfNotExist<TurretRotationSystem>(targetGameObject);
        }
    }


    private void AddComponentIfNotExist<T>(GameObject gameObject)
        where T : MonoBehaviour
    {
        if (gameObject.TryGetComponent<T>(out _) == false)
        {
            Undo.AddComponent(gameObject, typeof(T));
        }
    }
}
