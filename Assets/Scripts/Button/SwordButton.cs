using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordButton : Item
{
    bool have=false;
    public override void OnClick()
    {
        if (have == false)
        {
            item = new GameObject();
            item.AddComponent<SwordPatton>().Init(data);
            have = true;
        }
        else
        {
            item.GetComponent<SwordPatton>().LevelUp(NextDamage(), NextCount());
            level++;
        }
    }
}
