using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealButton : Item
{
    public override void OnClick()
    {
        GameManager.instance.player.health = GameManager.instance.player.playerData.maxHealth;
    }
    private void OnEnable()
    {
        textDesc.text = string.Format(data.itemDes);
    }
}
