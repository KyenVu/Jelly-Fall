using System.Collections.Generic;
using UnityEngine;

public enum PlatformType
{
    Normal,
    Moving,
    Breakable,
    Short
}

public class PlatformPool : MonoBehaviour
{
    [System.Serializable]
    public struct PlatformPrefab
    {
        public PlatformType type;
        public GameObject prefab;
    }

    public PlatformPrefab[] platformPrefabs;
    public int poolSize = 10;

    private Dictionary<GameObject, PlatformType> pool;
    private Dictionary<PlatformType, GameObject> prefabLookup;

    void Start()
    {
        pool = new Dictionary<GameObject, PlatformType>();
        prefabLookup = new Dictionary<PlatformType, GameObject>();

        foreach (var platform in platformPrefabs)
        {
            prefabLookup[platform.type] = platform.prefab;
        }

        // Initialize the pool with different platform types
        for (int i = 0; i < poolSize; i++)
        {
            foreach (var platform in platformPrefabs)
            {
                GameObject obj = Instantiate(platform.prefab);
                obj.SetActive(false);
                pool.Add(obj, platform.type);
            }
        }
    }

    public GameObject GetPlatform(PlatformType type)
    {
        foreach (KeyValuePair<GameObject, PlatformType> pair in pool)
        {
            if (!pair.Key.activeInHierarchy && pair.Value == type)
            {
                pair.Key.SetActive(true);
                return pair.Key;
            }
        }

        // If no inactive platform is found, instantiate a new one
        if (prefabLookup.ContainsKey(type))
        {
            GameObject newPlatform = Instantiate(prefabLookup[type]);
            pool.Add(newPlatform, type);
            return newPlatform;
        }

        return null;
    }

    public void ReturnPlatform(GameObject platform)
    {
        platform.SetActive(false);
        pool[platform] = PlatformType.Normal;
    }
}
