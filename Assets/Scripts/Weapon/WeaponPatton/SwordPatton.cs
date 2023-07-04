using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordPatton : WeaponPoint
{
    string keyDamage;
    string keyCount;
    string keyWeapon;
    string keySpeed;

    private void Update()
    {
        transform.Rotate(Vector3.back * speed* Time.deltaTime);
    }
    public override void Init(ItemData data)
    {
        base.Init(data);
        name = "SwordPoint";
        GameObject parentObject = GameObject.Find("WeaponPoint");
        transform.parent = parentObject.transform;
        transform.position = parentObject.transform.position;
        keyDamage = data.project.name + "Damage";
        keyCount = data.project.name + "Count";
        keyWeapon = data.project.name + "Project";
        keySpeed = data.project.name + "Speed";
        speed = speeds[keySpeed];
        Batch();
    }
    public void Batch()
    {

        float damage = damages[keyDamage];
        float count = counts[keyCount];
        GameObject weaponData = weaponDatas[keyWeapon];
        
        for (int i = 0; i < count; i++)
        {
            Transform weapon;
            if (i < transform.childCount)
            {
                weapon =  transform.GetChild(i);
            }
            else
            {
                weapon = GameManager.poolManager.Get(weaponData).transform;
                weapon.parent = transform;
            }

            weapon.localPosition = Vector3.zero;
            weapon.localRotation = Quaternion.identity;
            Vector3 rotate = (Vector3.forward * 360 * i / count);
            weapon.Rotate(rotate);
            weapon.Translate(weapon.up * 3f, Space.World);
            weapon.GetComponent<Weapon>().Init(damage, -1);
        }
    }
    public void LevelUp(float damage, float count)
    {
        damages[keyDamage] = damage;
        counts[keyCount] += count;
        Batch();
    }
}
