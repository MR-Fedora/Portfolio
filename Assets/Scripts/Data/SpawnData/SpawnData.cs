using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Spawn", menuName = "Script Object/SpawnData")]
public class SpawnData : ScriptableObject
{
    public GameObject monster;
    public int spawnType;
    public float spawnTime;
    public int health;
    public float speed;
}
