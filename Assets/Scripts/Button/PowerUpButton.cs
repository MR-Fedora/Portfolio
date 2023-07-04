using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpButton : Item
{
    public override void OnClick()
    {
        if (level == 0)
        {
            item = new GameObject();
            item.AddComponent<PowerUpGear>().Init(data);
        }
        else
        {
            item.GetComponent<PowerUpGear>().LevelUP(level);

        }
        level++;
    }
}
