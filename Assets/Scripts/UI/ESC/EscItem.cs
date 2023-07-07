using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static Cinemachine.DocumentationSortingAttribute;

public class EscItem : MonoBehaviour
{
    public Item[] items;
    public List<Item> itemHave;
    public Slot[] slots;

    private void Awake()
    {
        itemHave = new List<Item>();
    }

    public void Inventory()
    {
        for(int i=0;i< items.Length;i++)
        {
            if (items[i].have == true && items[i].inventory==false)
            {
                itemHave.Add(items[i]);
                items[i].inventory =true;
            }
        }
        for(int i=0;i<slots.Length;i++)
        {
            if (slots[i].contain==false)
            {
                if (itemHave.Count <= i)
                {
                    break;
                }
                slots[i].gameObject.SetActive(true);
                slots[i].image.sprite = itemHave[i].data.itemIcon;
                switch(itemHave[i].data.itemType)
                {
                    case ItemData.ItemType.Weapon:
                        if (itemHave[i].level == 0)
                            slots[i].text.text = "";
                        break;
                    case ItemData.ItemType.Gear:
                        slots[i].text.text = "LV." + itemHave[i].level;
                        break;
                }
                
                slots[i].contain = true;
                
            }
            else if (slots[i].contain == true)
            {
                switch (itemHave[i].data.itemType)
                {
                    case ItemData.ItemType.Weapon:
                        if (itemHave[i].level==4)
                            slots[i].text.text = "LV. MAX";
                        else
                            slots[i].text.text = "LV." + itemHave[i].level;
                        break;
                    case ItemData.ItemType.Gear:
                        if (itemHave[i].level == 4)
                            slots[i].text.text = "LV. MAX";
                        else
                            slots[i].text.text = "LV." + itemHave[i].level;
                        break;
                }
            }
        }
    }
}
