using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Cinemachine.DocumentationSortingAttribute;

public class PowerUpGear : Gear
{
    string key;
    ItemData data;
    public override void Init(ItemData data)
    {
        base.Init(data);
        name = data.name;
        key = data.name;
        this.data = data;
        GameObject parentObject = GameObject.Find("Gear");
        transform.parent = parentObject.transform;
        transform.position = parentObject.transform.position;
        PowerUp();
    }

    void PowerUp()
    {
        Weapon[] weapons = GameManager.instance.player.GetComponentsInChildren<Weapon>();
        float baseDamage = GameManager.instance.player.playerData.playerBaseDamage;
        baseDamage = baseDamage + baseDamage * gearCounts[key];
        GameManager.instance.player.playerDamage = baseDamage;
        foreach(Weapon weapon in weapons)
        {
            weapon.DamageRelocation();
        }
    }

    public void LevelUP(int level)
    {
        float count = data.damages[level];
        gearCounts[key] = count;
        PowerUp();
    }
}
