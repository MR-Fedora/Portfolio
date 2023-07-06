using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public ItemData data;
    public int level;
    public GameObject item;
    public UnityEvent newEvent;
    public bool max;

    public bool inventory=false;
    public bool have;
    public Image icon;
    public TMP_Text textLevel;
    public TMP_Text textName;
    public TMP_Text textDesc;

    private void Awake()
    {
        icon = GetComponentsInChildren<Image>()[1];
        icon.sprite = data.itemIcon;
        level = 0;
        TMP_Text[] texts = GetComponentsInChildren<TMP_Text>();
        textLevel = texts[0];
        textName = texts[1];
        textDesc = texts[2];
        textName.text = data.itemName;
    }
    protected float  NextDamage()
    {
        float nextDamage = data.baseDamage;
        nextDamage += nextDamage * data.damages[level];
        return nextDamage;
    }

    protected float NextCount()
    {
        float nextCount = 0;
        nextCount += data.count[level];

        return nextCount;
    }

    public virtual void OnClick() { }
}
