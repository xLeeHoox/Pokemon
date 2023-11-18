using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : SingletonMonoBehavior<PoolManager>
{
    [System.Serializable]
    struct PoolObject
    {
        public GameObject poolPrefab;
        public int poolSize;
    }


    [SerializeField] List<PoolObject> poolObjects;

    Dictionary<int, Queue<GameObject>> myDictionary = new Dictionary<int, Queue<GameObject>>();
    public void Start()
    {
        CreatePool();
    }
    public void CreatePool()
    {
        foreach (var item in poolObjects)
        {
            GameObject newGameObject = new GameObject();
            newGameObject.name = item.poolPrefab.name;
            newGameObject.transform.SetParent(this.transform);
            int prefabKey = item.poolPrefab.GetInstanceID();
            if (!myDictionary.ContainsKey(prefabKey))
            {
                myDictionary.Add(prefabKey, new Queue<GameObject>());
                for (int i = 0; i < item.poolSize; i++)
                {
                    GameObject newPoolPrefab = Instantiate(item.poolPrefab);
                    newPoolPrefab.SetActive(false);
                    newPoolPrefab.transform.SetParent(newGameObject.transform);
                    myDictionary[prefabKey].Enqueue(newPoolPrefab);
                }
            }
        }
    }
    public GameObject ReuseGameObject(GameObject gameObjectPrefab, Vector2 position)
    {
        int prefabKey = gameObjectPrefab.GetInstanceID();
        if (myDictionary.ContainsKey(prefabKey))
        {
            GameObject newGameObject = myDictionary[prefabKey].Dequeue();
            if (newGameObject.activeSelf)
            {
                newGameObject.SetActive(false);
            }
            myDictionary[prefabKey].Enqueue(newGameObject);
            ResetPrefab(newGameObject, position, gameObjectPrefab);
            return newGameObject;
        }
        else
        {
            Debug.Log("prefab haven't existed in pool");
            return null;
        }

    }

    private void ResetPrefab(GameObject gameObject, Vector2 position, GameObject gameObjectPrefab)
    {
        gameObject.transform.position = position;
        gameObject.transform.localScale = gameObjectPrefab.transform.localScale;
    }
}
