using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Player", menuName = "Script Object/PlayerData")]
public class PlayerData : ScriptableObject
{
    public float playerBaseDamage;
    public float maxHealth;
    public float speed;
    public int playerID;
    public int weaponID;
}
