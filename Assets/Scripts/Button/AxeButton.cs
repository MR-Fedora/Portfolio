using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeButton : Item
{
    bool have = false;
    public override void OnClick()
    {
        if(have == false)
        {
            item = new GameObject();
            item.AddComponent<AxePatton>().Init(data);
            have = true;
        }
        else
        {
            item.GetComponent<AxePatton>().LevelUp(NextDamage(), NextCount());
            level++;
        }
    }
}
