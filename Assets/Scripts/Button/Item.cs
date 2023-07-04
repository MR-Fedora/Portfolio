using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public ItemData data;
    public int level;
    public GameObject item;


    Image icon;
    TMP_Text textLevel;
    TMP_Text textName;
    TMP_Text textDesc;

    private void Awake()
    {
        icon = GetComponentsInChildren<Image>()[1];
        icon.sprite = data.itemIcon;
        level = 0;
        TMP_Text[] texts = GetComponentsInChildren<TMP_Text>();
        textLevel = texts[0];
        textName = texts[1];
        textDesc = texts[2];
        textName.text = data.itemName;
    }

    private void OnEnable()
    {
        textLevel.text = "Lv." + (level + 1);
        if (level == data.damages.Length)
        {
            textLevel.text = "Lv. MAX";
        }
        switch (data.itemType)
        {
            case ItemData.ItemType.Sword:
            case ItemData.ItemType.Fire:
            case ItemData.ItemType.Axe:
                textDesc.text = string.Format(data.itemDes, data.damages[level]*100, data.count[level]);
                break;
            case ItemData.ItemType.Speed:
            case ItemData.ItemType.UPGrade:
                textDesc.text = string.Format(data.itemDes, data.damages[level]*100);
                break;
            default:
                textDesc.text = string.Format(data.itemDes);
                break;
        }
    }
    protected float  NextDamage()
    {
        float nextDamage = data.baseDamage;
        nextDamage += nextDamage * data.damages[level];
        return nextDamage;
    }

    protected float NextCount()
    {
        float nextCount = 0;
        nextCount += data.count[level];

        return nextCount;
    }

    public virtual void OnClick() { }
}
