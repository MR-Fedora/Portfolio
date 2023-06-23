using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item",menuName ="Script Object/ItemData")]
public class ItemData : ScriptableObject
{
    public enum ItemType { Melee, Range, UPGrade, Speed, Heal}

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
    public float[] damages;
    public int[] count;

    [Header("Weapon")]
    public GameObject project;
}