using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class WeaponPoint : MonoBehaviour
{
    public WeaponData[] weaponData;
 
    private float damage;
    private int count;
    private float speed;

    float timer;
    PlayerMove player;
    private void Awake()
    {
        player = GetComponentInParent<PlayerMove>();
        Init();
    }
    private void Update()
    {

        transform.Rotate(Vector3.back * weaponData[0].speed * Time.deltaTime);

        timer += Time.deltaTime;
        if (timer > weaponData[1].speed)
        {
            timer = 0f;
            Fire();
        }
    }
    public void LevelUp(float damage,int count)
    {
        this.damage = damage;
        this.count = count;

        Batch();
    }
    public void Init()
    {  
        Batch();
    }
    private void Batch()
    {
        
        for (int i = 0; i < weaponData[0].count; i++)
        {
            GameObject sword = GameManager.poolManager.Get<GameObject>(weaponData[0].weapon);
            Transform weapon;
            if(i<transform.childCount)
            {
                weapon = transform.GetChild(i);
            }
            else
            {
                weapon = sword.transform;
                weapon.parent = transform;
            }

            weapon.localPosition = Vector3.zero;
            weapon.localRotation = Quaternion.identity;
           
            Vector3 rotate = (Vector3.forward * 360 * i / weaponData[0].count);
            weapon.Rotate(rotate);
            weapon.Translate(weapon.up * 1.5f, Space.World);
            weapon.GetComponent<Weapon>().Init(weaponData[0].damage, weaponData[0].per);
        }
    }

    private void Fire()
    {
        if (!player.scanner.nearestTarget)
            return;
        Vector3 targetPos = player.scanner.nearestTarget.position;
        Vector3 dir = targetPos - transform.position;
        dir = dir.normalized;

        GameObject fireball = GameManager.poolManager.Get<GameObject>(weaponData[1].weapon);
        Transform weapon = fireball.transform;
        weapon.position = transform.position;
        weapon.rotation = Quaternion.FromToRotation(Vector3.up, dir);
        weapon.GetComponent<Weapon>().Init(weaponData[1].damage, weaponData[1].per, dir);
    }

}

