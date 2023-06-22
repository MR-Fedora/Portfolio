using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float damage;
    public float per;
    public bool isDied;

    Rigidbody2D rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    public void Init(float damage, float per, Vector3 dir)
    {
        this.damage = damage;
        this.per = per;
        this.isDied = false;

        if (per > -1)
        {
            rb.velocity = dir * 10;
        }
    }

    public void Init(float damage, float per)
    {
        Init(damage, per, Vector3.zero);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy") || per == -1)
            return;

        per--;

        if (per == -1)
        {
            rb.velocity = Vector2.zero;
            if (!isDied)
            {
                GameManager.poolManager.Release(gameObject);
                isDied = true;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Area") || per == -1) return;


        rb.velocity = Vector2.zero;
        if (!isDied)
        {
            GameManager.poolManager.Release(gameObject);
            isDied = true;
        }
    }
}