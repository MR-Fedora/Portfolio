using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AxePatton : WeaponPoint
{
    float timer;
    string keyDamage;
    string keyCount;
    string keyWeapon;
    string keySpeed;
    GameObject weaponData;
    float count;
    float damage;

    Animator ani;
    private void Update()
    {
        timer += Time.deltaTime;
        if(timer>speed)
        {
            timer = 0f;
            StartCoroutine(SwingRoutine());
        }
    }
    public override void Init(ItemData data)
    {
        base.Init(data);
        name = "AxePoint";
        GameObject parentObject = GameObject.Find("WeaponPoint");
        transform.parent = parentObject.transform;
        transform.position = parentObject.transform.position;
        keyDamage = data.project.name + "Damage";
        keyCount = data.project.name + "Count";
        keyWeapon = data.project.name + "Project";
        keySpeed = data.project.name + "Speed";
        weaponData = weaponDatas[keyWeapon];
        count = counts[keyCount];
        speed = speeds[keySpeed];
        Swing();
    }
    public void Swing()
    {
        damage = damages[keyDamage];
        Transform weapon;
        if (transform.childCount > 0)
        {
            weapon = transform.GetChild(0);
        }
        else
        {
            weapon = GameManager.poolManager.Get(weaponData).transform;
            weapon.parent = transform;
            weapon.gameObject.SetActive(false);
        }
        if (weapon.gameObject.activeSelf == false)
        {
            weapon.position = transform.position;
        }
        weapon.transform.localScale = Vector3.one * count;
        weapon.GetComponent<Weapon>().Init(damage, -1);
        weaponData = weapon.gameObject;
    }

    IEnumerator SwingRoutine()
    {
        if (!GameManager.instance.player.scanner.swingTarget)
        {
            yield break;
        }
        Vector3 targetPos = GameManager.instance.player.scanner.swingTarget.position;
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

    public void LevelUp(float damage, float count)
    {
        damages[keyDamage] = damage;
        counts[keyCount] += count;

        Swing();
        if (speed < 0.5)
            ani.speed = 2f;
    }
}
