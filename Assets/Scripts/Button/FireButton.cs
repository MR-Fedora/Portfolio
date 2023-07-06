using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FireButton : Item
{
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
            if (level > 4)
            {
                level = 4;
                max = true;
                gameObject.GetComponent<Button>().interactable = false;
            }
        }
        newEvent.Invoke();
    }
    private void OnEnable()
    {
        if (have == false)
        {
            textLevel.text = " ";
            textDesc.text = "적을 향해 화염을 발사";
        }
        else
        {
            textLevel.text = "Lv." + (level + 1);
            textDesc.text = string.Format(data.itemDes, data.damages[level] * 100, data.count[level]);
        }

    }
}
