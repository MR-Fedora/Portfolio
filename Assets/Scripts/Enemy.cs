using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    PlayerMove target;
    public float health;
    public float maxHealth;
    bool isLive;
    Rigidbody2D rb;
    SpriteRenderer sprite;
    Animator ani;
    Collider2D coll;
    WaitForFixedUpdate wait;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        ani = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();
        wait = new WaitForFixedUpdate();
    }

    private void FixedUpdate()
    {
        if (!GameManager.instance.isLive)
            return;
        if (!isLive||ani.GetCurrentAnimatorStateInfo(0).IsName("Hit"))
        {
            return;
        }
            
        Vector2 dir = target.transform.position-transform.position;
        Vector2 nextVec =dir.normalized*moveSpeed*Time.fixedDeltaTime;
        rb.MovePosition(rb.position + nextVec);
        rb.velocity = Vector2.zero;
    }

    private void LateUpdate()
    {
        if (!GameManager.instance.isLive)
            return;
        if (!isLive)
            return;
        sprite.flipX = target.transform.position.x < transform.position.x;
    }

    private void OnEnable()
    {
        target = GameManager.instance.player;
        isLive = true;
        health = maxHealth;
    }

    public void Init(SpawnData data)
    {
        moveSpeed = data.speed; 
        maxHealth = data.health;
        health=data.health;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Weapon")||!isLive)
            return;
        health -= collision.GetComponent<Weapon>().damage;
        if (health>0)
        {
            ani.SetTrigger("Hit");
            AudioManager.instance.PlaySFX(AudioManager.SFX.Hit);
        }
        else if(health <= 0)
        {
            Dead();
            GameManager.instance.kill++;
            GameManager.instance.GetExp();
            if(GameManager.instance.isLive)
            {
                AudioManager.instance.PlaySFX(AudioManager.SFX.MonsterDie);
            }
          
        }
    }
    IEnumerator KnockBack()
    {
        yield return wait;
        Vector3 playerPos = GameManager.instance.player.transform.position;
        Vector3 dir = transform.position - playerPos;
        rb.AddForce(dir.normalized * 3, ForceMode2D.Impulse);
    }
    private void Dead()
    {
        StartCoroutine(Dying());
    }
    private IEnumerator Dying()
    {
        ani.SetTrigger("Dying");
        moveSpeed = 0;
        coll.enabled=false;
        yield return new WaitForSeconds(0.3f);
        coll.enabled = true;
        GameManager.poolManager.Release<GameObject>(gameObject);

    }
}
