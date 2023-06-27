using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class WeaponPoint : MonoBehaviour
{
    private ItemData data;
    GameObject weaponData;
    private float damage;
    private int count;
    public float speed;
    public int id;
    private bool active;

    float timer;
    PlayerMove player;
    private void Awake()
    {
        player = GameManager.instance.player;
        
    }
    private void Update()
    {
        if (!GameManager.instance.isLive)
            return;
        switch (id)
        {
            case 0:
                transform.Rotate(Vector3.back * speed * Time.deltaTime);
                break;
            case 1:
                timer += Time.deltaTime;
                if (timer > speed)
                {
                    timer = 0f;
                    Fire();
                }
                break;
        }
    }
    public void LevelUp(float damage, int count)
    {
        this.damage = damage;
        this.count += count;

        if (id == 0)
        {
            Batch();

        }
        player.BroadcastMessage("ApplyGear", SendMessageOptions.DontRequireReceiver);
    }
    public void Init(ItemData data)
    {
        name = "WeaponPoint"+data.itemId;
        transform.parent=player.transform;
        transform.localPosition = Vector3.zero;
        
        weaponData = data.project;

        id=data.itemId;
        damage = data.baseDamage;
        count = data.baseCount;

        switch (id)
        {
            case 0:
                speed = 150;
                Batch();
                break;
            case 1:
                speed = 0.3f;
                break;

        }
        player.BroadcastMessage("ApplyGear",SendMessageOptions.DontRequireReceiver);
    }
    private void Batch()
    {
        for (int i = 0; i < count; i++)
        {
            Transform weapon;
            if (i < transform.childCount)
            {
                weapon = transform.GetChild(i);
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
            weapon.Translate(weapon.up * 1.5f, Space.World);
            weapon.GetComponent<Weapon>().Init(damage, -1);
        }
    }

    private void Fire()
    {
        if (!player.scanner.nearestTarget)
            return;
        Vector3 targetPos = player.scanner.nearestTarget.position;
        Vector3 dir = targetPos - transform.position;
        dir = dir.normalized;

        GameObject fireball = GameManager.poolManager.Get(weaponData);
        Transform weapon = fireball.transform;
        weapon.position = transform.position;
        weapon.rotation = Quaternion.FromToRotation(Vector3.up, dir);
        weapon.GetComponent<Weapon>().Init(damage, 1, dir);

        AudioManager.instance.PlaySFX(AudioManager.SFX.Fire);
    }
}

