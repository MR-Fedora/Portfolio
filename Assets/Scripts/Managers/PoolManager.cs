using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class PoolManager : MonoBehaviour
{
    Dictionary<string, ObjectPool<GameObject>> poolDic;

    private void Awake()
    {
        poolDic = new Dictionary<string, ObjectPool<GameObject>>();
    }

    public GameObject Get(GameObject prefab)
    {
        if (!poolDic.ContainsKey(prefab.name))
            CreatePool(prefab.name, prefab);
        ObjectPool<GameObject> pool = poolDic[prefab.name];
        return pool.Get();
    }

    public bool Release(GameObject go)
    {
        if (!poolDic.ContainsKey(go.name))
            return false;
        ObjectPool<GameObject> pool = poolDic[go.name];
        pool.Release(go);
        return true;
    }

    private void CreatePool(string key ,GameObject prefab)
    {
        ObjectPool<GameObject> pool = new ObjectPool<GameObject>(
            createFunc: () =>
            {
                GameObject go = Instantiate(prefab);
                go.name = key;
                return go;
            },
            actionOnGet: (GameObject go) =>
            {
                go.SetActive(true);
                go.transform.SetParent(null);
            },
            actionOnRelease: (GameObject go) =>
            {
                go.SetActive(false);
                go.transform.SetParent(transform);
            },
            actionOnDestroy: (GameObject go) =>
            {
                Destroy(go);
            }
            );
        poolDic.Add(key, pool);
    }
}
