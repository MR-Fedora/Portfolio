using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedButton : Item
{
    public override void OnClick()
    {
       if(level == 0)
        {
            item = new GameObject();
            item.AddComponent<SpeedGear>().Init(data);
        }
       else
        {
            item.GetComponent<SpeedGear>().LevelUP(level);
            
        }
        level++;
    }
}
