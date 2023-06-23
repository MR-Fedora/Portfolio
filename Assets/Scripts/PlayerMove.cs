using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] public float moveSpeed;
    [SerializeField] public Result overUI;
    [SerializeField] public GameObject enemyClear;

    public int level;
    public Scanner scanner;
    private GameObject weapon;
    private Animator ani;
    private SpriteRenderer sprite;
    private Rigidbody2D rb;
    public Vector2 moveDir;
    private Spawner spawner;

    private void Awake()
    {
        ani = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        scanner=GetComponent<Scanner>();
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
    private void OnCollisionStay2D(Collision2D collision)
    {
        if(!GameManager.instance.isLive)
            return;
        GameManager.playerData.health -= Time.deltaTime*30f;

        if(GameManager.playerData.health<=0)
        {
            for(int i=2;i<transform.childCount;i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);

            }
            ani.SetTrigger("Die");
            GameManager.instance.GameOver();
        }
    }
}
