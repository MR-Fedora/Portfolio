using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwordButton : Item
{
    
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
            if(level >4)
            {

                max = true;
                gameObject.GetComponent<Button>().interactable = false;
            }
        }
        newEvent.Invoke();
    }
    private void OnEnable()
    {
        if(have == false)
        {
            textLevel.text = " ";
            textDesc.text = "회전하는 검을 소환";
        }
        else
        {
            if (level == 4)
                textLevel.text = "LV. MAX";
            else
                textLevel.text = "Lv." + (level + 1);
            textDesc.text = string.Format(data.itemDes, data.damages[level] * 100, data.count[level]);
        }
        
    }
}
