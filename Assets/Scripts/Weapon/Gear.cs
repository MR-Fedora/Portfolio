using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Gear : MonoBehaviour
{
    public ItemData.ItemType type;
    public float meleeRate;
    public float rangeRate;

    public void Init(ItemData data)
    {
        name = "Gear" +data.itemId;
        transform.parent=GameManager.instance.player.transform;
        transform.localPosition = Vector3.zero;

        type = data.itemType;
        meleeRate = data.damages[0];
        rangeRate = data.count[0];
        ApplyGear();
    }

    public void LevelUp(float meleeRate,float rangeRate)
    {
        this.meleeRate = meleeRate;
        this.rangeRate = rangeRate;
        ApplyGear();
    }

    void ApplyGear()
    {
        switch(type)
        {
            case ItemData.ItemType.UPGrade:
                RateUp();
                break;
            case ItemData.ItemType.Speed:
                SpeedUp();
                break;
        }
    }
    void RateUp()
    {
        WeaponPoint[] weapons = transform.parent.GetComponentsInChildren<WeaponPoint>();

        foreach (WeaponPoint weapon in weapons)
        {
            switch(weapon.id)
            {
                case 0:
                    weapon.speed = 150 + (150 * meleeRate);
                    break;
                case 1:
                    weapon.speed = 0.5f * (1f - rangeRate);
                    break;
                case 2:
                    if(weapon.speed<0.5f)
                    {
                        weapon.GetComponent<Animator>();
                        weapon.speed = 2f;
                    }
                    weapon.speed = 1f * (1f - rangeRate);
                    break;
            }
        }
    }

    void SpeedUp()
    {
        float speed = GameManager.instance.player.playerData.speed;
        GameManager.instance.player.moveSpeed = speed + speed * meleeRate;
    }
}
