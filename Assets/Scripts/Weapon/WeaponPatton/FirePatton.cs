using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePatton : WeaponPoint
{
    float timer;
    string keyDamage;
    string keyCount;
    string keyWeapon;
    string keySpeed;

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer > speeds[keySpeed])
        {
            timer = 0;
            Fire();
        }
    }
    public override void Init(ItemData data)
    {
        base.Init(data);
        name = "FirePoint";
        GameObject parentObject = GameObject.Find("WeaponPoint");
        transform.parent = parentObject.transform;
        transform.position = parentObject.transform.position;
        keyDamage = data.project.name + "Damage";
        keyCount = data.project.name + "Count";
        keyWeapon = data.project.name + "Project";
        keySpeed = data.project.name + "Speed";
        speed = speeds[keySpeed];
        Fire();
    }
    private void Fire()
    {
        float damage = damages[keyDamage];
        float count = counts[keyCount];
        GameObject weaponData = weaponDatas[keyWeapon];

        if (!GameManager.instance.player.scanner.FireTarget)
            return;
        Vector3 targetPos = GameManager.instance.player.scanner.FireTarget.position;
        Vector3 dir = targetPos - transform.position;
        dir = dir.normalized;

        GameObject fireball = GameManager.poolManager.Get(weaponData);
        Transform weapon = fireball.transform;
        weapon.parent = transform;
        weapon.position = transform.position;
        weapon.rotation = Quaternion.FromToRotation(Vector3.up, dir);
        weapon.GetComponent<Weapon>().Init(damage, count, dir);

        AudioManager.instance.PlaySFX(AudioManager.SFX.Fire);
    }

    public void LevelUp(float damage, float count)
    {
        damages[keyDamage] = damage;
        counts[keyCount] += count;
    }
}
