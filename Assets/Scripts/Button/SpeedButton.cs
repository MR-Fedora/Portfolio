using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeedButton : Item
{
    public override void OnClick()
    {
       if(level == 0)
        {
            item = new GameObject();
            item.AddComponent<SpeedGear>().Init(data);
            have = true;
        }
       else
        {
            item.GetComponent<SpeedGear>().LevelUP(level);
            
        }
        level++;
        if (level > 4)
        {
            level = 4;
            max = true;
            gameObject.GetComponent<Button>().interactable = false;
        }
        newEvent.Invoke();
    }
    private void OnEnable()
    {
        textLevel.text = "Lv." + (level + 1);
        textDesc.text = string.Format(data.itemDes, data.damages[level] * 100);
    }
}
