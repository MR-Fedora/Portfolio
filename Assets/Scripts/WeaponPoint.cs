using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPoint : MonoBehaviour
{
    public int id;
    public int prefabId;
    public float damage;
    public int count;
    public float speed;

    public void Init()
    {
        switch (id)
        {
            case 0:
                speed = -150;
                Batch();
                break;
            case 1:
                break;
            default:
                break;
        }
    }

    private void Batch()
    {
        for (int i = 0; i < count; i++)
        {
            //GameManager.instance.pool.Get(i);
        }
    }
}
