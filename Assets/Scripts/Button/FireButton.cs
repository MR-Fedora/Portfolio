using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireButton : Item
{
    bool have = false;
    public override void OnClick()
    {
        if(have == false)
        {
            item = new GameObject();
            item.AddComponent<FirePatton>().Init(data);
            have = true;
        }
        else
        {
            item.GetComponent<FirePatton>().LevelUp(NextDamage(), NextCount());
            level++;
        }
    }
}
