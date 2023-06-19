using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int damage;
    public float per;

    public void Init(int damage, float per)
    {
        this.damage = damage;
        this.per = per;
    }
}
