using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Gear : MonoBehaviour
{
    protected Dictionary<string, float> gearCounts;

    private void Awake()
    {
        gearCounts = new Dictionary<string, float>();
    }
    public virtual void Init(ItemData data)
    {
        string key = data.name;
        float count = data.damages[0];
        gearCounts.Add(key, count);
    }  
}
