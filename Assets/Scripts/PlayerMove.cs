using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] public float moveSpeed;

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
        level=GameManager.instance.level;
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
}
