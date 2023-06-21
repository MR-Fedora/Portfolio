using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public int health;
    public int maxHealth=100;

    private void Start()
    {
        health = maxHealth;
    }
}
