using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public ItemData data;
    public int level;
    public WeaponPoint weapon;
    public Gear gear;


    Image icon;
    TMP_Text textLevel;
    TMP_Text textName;
    TMP_Text textDesc;

    private void Awake()
    {
        icon = GetComponentsInChildren<Image>()[1];
        icon.sprite = data.itemIcon;

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
            case ItemData.ItemType.Melee:
            case ItemData.ItemType.Range:
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
    public void OnClick()
    {
        switch(data.itemType)
        {
            case ItemData.ItemType.Melee:
            case ItemData.ItemType.Range:
                if (level == 0)
                {
                    GameObject newWeapon = new GameObject();
                    weapon = newWeapon.AddComponent<WeaponPoint>();
                    weapon.Init(data);
                }
                else
                {
                    float nextDamage = data.baseDamage;
                    int nextCount = 0;

                    nextDamage += data.baseDamage * data.damages[level];
                    nextCount += data.count[level];

                    weapon.LevelUp(nextDamage, nextCount);
                }
                level++;
                break;
            
            case ItemData.ItemType.Speed:
            case ItemData.ItemType.UPGrade:
                if(level == 0)
                {
                    GameObject newGear = new GameObject();
                    gear = newGear.AddComponent<Gear>();
                    gear.Init(data);
                }
                else
                {
                    float nextRate = data.damages[level];
                    gear.LevelUp(nextRate);
                }
                level++;
                break;
            case ItemData.ItemType.Heal:
                GameManager.playerData.health = GameManager.playerData.maxHealth;

                break;
        }
        

        if(level == data.damages.Length)
        {
            GetComponent<Button>().interactable = false;
        }
    }
}
