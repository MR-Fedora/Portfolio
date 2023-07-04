using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Cinemachine.DocumentationSortingAttribute;

public class SpeedGear : Gear
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
        SpeedUp();
    }
    void SpeedUp()
    {
        float speed = GameManager.instance.player.playerData.speed;
        Debug.Log(gearCounts[key]);
        GameManager.instance.player.moveSpeed = speed + speed * gearCounts[key];
        
    }

    public void LevelUP(int levelUP)
    {
        float count = data.damages[levelUP];
        gearCounts[key] = count;
        SpeedUp();
    }
}
