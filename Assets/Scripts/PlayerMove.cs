using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed;
    public Result overUI;
    public GameObject enemyClear;
    public PlayerData playerData;

    public int level;
    public Scanner scanner;
    private GameObject weapon;
    private Animator ani;
    private SpriteRenderer sprite;
    private Rigidbody2D rb;
    public Vector2 moveDir;
    public float health;
    public float playerDamage;

    private void Awake()
    {
        moveSpeed = playerData.speed;
        ani = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        scanner=GetComponent<Scanner>();
        health = playerData.maxHealth;
        playerDamage = playerData.playerBaseDamage;

    }
    private void FixedUpdate()
    {
        if (!GameManager.instance.isLive)
            return;
        level =GameManager.instance.level;
        Move();
    }
    private void Move()
    {
        if(moveDir.x<0)
        {
            sprite.flipX = true;
        }
        else if(moveDir.x>0)
        {
            sprite.flipX= false;
        }
        Vector2 dir= moveDir*moveSpeed*Time.fixedDeltaTime;
        rb.MovePosition(dir+rb.position);
        ani.SetFloat("Move", moveDir.magnitude);
    }
    private void OnMove(InputValue value)
    {
        moveDir = value.Get<Vector2>();
    }

    //private void OnMenu(InputValue value)
    //{
    //        GameManager.instance.escMenu.Show();
    //}
    private void OnCollisionStay2D(Collision2D collision)
    {
        if(!GameManager.instance.isLive)
            return;
        health -= Time.deltaTime*30f;

        if(health<=0)
        {
            for(int i=2;i<transform.childCount;i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);

            }
            ani.SetTrigger("Die");
            GameManager.instance.GameOver();
            if (playerData.playerID == 0 || playerData.playerID == 2)
                AudioManager.instance.PlaySFX(AudioManager.SFX.SoldierDie);
            else
                AudioManager.instance.PlaySFX(AudioManager.SFX.WizardDid);
        }
    }
}
