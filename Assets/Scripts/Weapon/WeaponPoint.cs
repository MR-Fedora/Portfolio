using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class WeaponPoint : MonoBehaviour
{ 
    protected Dictionary<string,GameObject> weaponDatas;
    protected Dictionary<string, float> damages;
    protected Dictionary<string, float> counts;
    protected Dictionary<string, float> speeds;
    public float speed;
    public int id;
    PlayerMove player;

    private void Awake()
    {
        player = GameManager.instance.player;
        weaponDatas = new Dictionary<string, GameObject>();
        damages = new Dictionary<string, float>();
        counts = new Dictionary<string, float>();
        speeds = new Dictionary<string, float>();
    }
    public virtual void Init(ItemData data)
    {
        string keyDamage =data.project.name+"Damage";
        float damage = data.baseDamage;
        damages.Add(keyDamage, damage);

        string keyCount = data.project.name + "Count";
        float count = data.baseCount;
        counts.Add(keyCount, count);

        string KeyProject = data.project.name + "Project";
        GameObject weaponData = data.project;
        weaponDatas.Add(KeyProject, weaponData);

        string KeySpeed = data.project.name + "Speed";
        float speed = data.baseSpeed;
        speeds.Add(KeySpeed, speed);

        player.BroadcastMessage("ApplyGear",SendMessageOptions.DontRequireReceiver);
    }
}