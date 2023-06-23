using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public float health;
    public float maxHealth=100;

    private void Start()
    {
        health = maxHealth;
    }
}
