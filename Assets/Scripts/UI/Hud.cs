using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Cinemachine.DocumentationSortingAttribute;

public class Hud : MonoBehaviour
{
    public enum InfoType { EXP, Level, Kill, Time, Health }
    public InfoType type;

    TMP_Text text;
    Slider slider;

    private void Awake()
    {
        text = GetComponent<TMP_Text>();
        slider = GetComponent<Slider>();
    }
    
    private void LateUpdate()
    {
        switch(type)
        {
            case InfoType.EXP:
                float curExp = GameManager.instance.exp;
                float maxExp = GameManager.instance.nextExp[Mathf.Min(GameManager.instance.level, GameManager.instance.nextExp.Length - 1)];
                slider.value = curExp/maxExp;
                break;
            case InfoType.Level:
                text.text= string.Format("LV. {0:F0}",GameManager.instance.level+1);
                break;
            case InfoType.Kill:
                text.text = string.Format("{0:F0}", GameManager.instance.kill);
                break;
            case InfoType.Time:
                float remainTime = GameManager.instance.maxTime - GameManager.instance.gameTime;
                int min = Mathf.FloorToInt(remainTime/60);
                int sec = Mathf.FloorToInt(remainTime%60);
                text.text = string.Format("{0:D2} : {1:D2}", min, sec);
                break;
            case InfoType.Health:
                float curHealth = GameManager.instance.player.health;
                float maxHealth = GameManager.instance.player.playerData.maxHealth;
                slider.value = curHealth / maxHealth;
                break;
        }
    }
}
