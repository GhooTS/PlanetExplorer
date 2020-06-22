using System.Collections;
using UnityEngine;

[System.Serializable]
public class GameObjectPool<T> : IEnumerable where T : MonoBehaviour
{
    public int poolSize = 0;
    public T prefab;
    private int spawnObjects = 0;
    private T[] objectsPool;


    public void Intial()
    {
        objectsPool = new T[poolSize];
        spawnObjects = 0;
    }

    public T RequestObject(Vector3 spawnPosition, Quaternion rotation)
    {
        int index = FindFirstDisable();

        if (index != -1)
        {
            objectsPool[index].gameObject.SetActive(true);
            objectsPool[index].transform.position = spawnPosition;
            objectsPool[index].transform.rotation = rotation;
            return objectsPool[index];
        }
        else if (spawnObjects < poolSize)
        {
            return SpawnObject(spawnObjects, spawnPosition, rotation);
        }
        else
        {
            Debug.Log($"object pool is full");
            return null;
        }
    }


    IEnumerator IEnumerable.GetEnumerator()
    {
        return (IEnumerator)GetEnumerator();
    }

    public GameObjectPoolEnum<T> GetEnumerator()
    {
        return new GameObjectPoolEnum<T>(objectsPool);
    }


    public void Dispolse()
    {
        for (int i = 0; i < spawnObjects; i++)
        {
            GameObject.Destroy(objectsPool[i].gameObject);
        }
    }

    private T SpawnObject(int index, Vector3 spawnPosition, Quaternion rotation)
    {
        objectsPool[index] = GameObject.Instantiate(prefab, spawnPosition, rotation);
        spawnObjects++;
        return objectsPool[index];
    }

    private int FindFirstDisable()
    {
        for (int i = 0; i < spawnObjects; i++)
        {
            if (objectsPool[i].gameObject.activeSelf == false)
            {
                return i;
            }
        }

        return -1;
    }
}
