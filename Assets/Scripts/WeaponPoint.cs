using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponPoint : MonoBehaviour
{
    GameObject sword;
    public float damage;
    public int count;
    public float speed;

    private void Awake()
    {
        sword = GameManager.resourceManager.Load<GameObject>("Prefabs/Weapon/Sword");
     
        Init();
    }
    private void Update()
    { 
        transform.Rotate(Vector3.back * speed * Time.deltaTime);  
    }
    public void Init()
    {
        count = 4;
        speed = 150;
        Batch();
    }

    private void Batch()
    {
        
         for (int i = 0; i < count; i++)
         {
             Transform weapon = GameManager.resourceManager.Instantiate(sword, transform).transform;
             Vector3 rotate = (Vector3.forward * 360 * i / count);
             weapon.Rotate(rotate);
             weapon.Translate(weapon.up * 1.5f, Space.World);
             weapon.GetComponent<Weapon>();
          }
    }
}
