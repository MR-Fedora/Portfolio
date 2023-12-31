using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUp : MonoBehaviour
{
    RectTransform rect;
    public Item[] items;
    public HealButton heal;
    public bool on=false;
    private void Awake()
    {
        
        items = GetComponentsInChildren<Item>(true);
        rect = GetComponent<RectTransform>();
    }

    public void Show()
    {
        Next();
        on = true;
        rect.localScale = Vector3.one;
        GameManager.instance.Stop();
        
    }

    public void Hide()
    {
        on = false;
        rect.localScale = Vector3.zero; 
        GameManager.instance.Resume();
    }

    void Next()
    {
        foreach(Item item in items)
        {
            item.gameObject.SetActive(false);
        }

        int[] ran = new int[3];
        while(true)
        {
            ran[0] = Random.Range(0, items.Length);
            ran[1] = Random.Range(0, items.Length);
            ran[2] = Random.Range(0, items.Length);
            if (ran[0] != ran[1] && ran[1] != ran[2] && ran[0] != ran[2])
                break;
        }
        for (int i = 0; i < ran.Length; i++)
        {
            Item ranItem = items[ran[i]];
            if (ranItem.max)
            {
                heal.gameObject.SetActive(true);
            }
            else
            {
                ranItem.gameObject.SetActive(true);
            }
        }
    }
}
