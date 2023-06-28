using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class WeaponPoint : MonoBehaviour
{ 
    Animator ani;
    GameObject weaponData;
    private float damage;
    private float count;
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
            case 2:
                timer += Time.deltaTime;
                if (timer > speed)
                {
                    
                    timer = 0f;
                    StartCoroutine(SwingRoutine());
                }
                break;
        }
    }
    public void LevelUp(float damage, float count)
    {
        this.damage = damage;
        this.count += count;

        switch (id)
        {
            case 0:
                Batch();
                break;
            case 2:
                Swing(); 
                if(speed<0.5)
                    ani.speed = 2f;
                break;
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
            case 2:
                speed = 1f;
                Swing();
                weaponData.SetActive(false);
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
        if (!player.scanner.FireTarget)
            return;
        Vector3 targetPos = player.scanner.FireTarget.position;
        Vector3 dir = targetPos - transform.position;
        dir = dir.normalized;

        GameObject fireball = GameManager.poolManager.Get(weaponData);
        Transform weapon = fireball.transform;
        weapon.parent=transform;
        weapon.position = transform.position;
        weapon.rotation = Quaternion.FromToRotation(Vector3.up, dir);
        weapon.GetComponent<Weapon>().Init(damage, 1, dir);

        AudioManager.instance.PlaySFX(AudioManager.SFX.Fire);
    }
    public void Swing()
    { 
        Transform weapon;
        if (transform.childCount>0)
        {
            weapon = transform.GetChild(0);
        }
        else
        {
            weapon = GameManager.poolManager.Get(weaponData).transform;
            weapon.parent = transform;
        }
        if(weapon.gameObject.activeSelf==false)
        {
            weapon.position = transform.position;
        }
        weapon.transform.localScale = Vector3.one * count;
        weapon.GetComponent<Weapon>().Init(damage, -1);
        weaponData = weapon.gameObject;
    }

    IEnumerator SwingRoutine()
    {
        if(!player.scanner.swingTarget)
        {
            yield break;
        }
        Vector3 targetPos = player.scanner.swingTarget.position;
        Vector3 dir = targetPos - transform.position;
        dir = dir.normalized;
        weaponData.SetActive(true);
        weaponData.transform.position = transform.position;
        weaponData.transform.rotation = Quaternion.FromToRotation(Vector3.right, dir);
        weaponData.transform.Translate(weaponData.transform.right * count, Space.World);
        AudioManager.instance.PlaySFX(AudioManager.SFX.Swing);
        yield return new WaitForSeconds(0.5f);
        weaponData.SetActive(false);
        StopAllCoroutines();
    }
}

