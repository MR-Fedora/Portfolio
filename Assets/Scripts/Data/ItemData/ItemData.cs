using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item",menuName ="Script Object/ItemData")]
public class ItemData : ScriptableObject
{
    public enum ItemType { Weapon, Gear}

    [Header("Main Info")]
    public ItemType itemType;
    public int itemId;
    public string itemName;
    [TextArea]
    public string itemDes;
    public Sprite itemIcon;

    [Header("Level Data")]
    public float baseDamage;
    public int baseCount;
    public float baseSpeed;
    public float[] damages;
    public float[] count;

    [Header("Weapon")]
    public GameObject project;
}
