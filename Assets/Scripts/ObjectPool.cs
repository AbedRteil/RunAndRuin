using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool Instance;

    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }

    public List<Pool> pools;
    private Dictionary<string, Queue<GameObject>> poolDictionary;

    void Awake()
    {
        Instance = this;
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectQueue = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                objectQueue.Enqueue(obj);
            }

            poolDictionary.Add(pool.tag, objectQueue);
        }
    }

    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Pool with tag " + tag + " doesn't exist.");
            return null;
        }

        Queue<GameObject> queue = poolDictionary[tag];
        GameObject objectToSpawn;

        if (queue.Count > 0)
        {
            objectToSpawn = queue.Dequeue();
        }
        else
        {
            // Pool ran out — grow it dynamically instead of failing
            Pool poolConfig = pools.Find(p => p.tag == tag);
            objectToSpawn = Instantiate(poolConfig.prefab);
        }

        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;
        objectToSpawn.SetActive(true);

        return objectToSpawn;
    }

    public void ReturnToPool(string tag, GameObject obj)
    {
        obj.SetActive(false);

        if (poolDictionary.ContainsKey(tag))
        {
            poolDictionary[tag].Enqueue(obj);
        }
        else
        {
            Destroy(obj);
        }
    }
}