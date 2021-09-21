using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hero : MonoBehaviour
{
    [SerializeField] private float speed = 3f;
    [SerializeField] private float jumpForce = 15f;

    [SerializeField] private float circleRadius = 0.85f;

    [SerializeField] private Image[] hearts;

    [SerializeField] private Sprite fullHeart;
    [SerializeField] private Sprite emptyHeart;

    [SerializeField] private GameObject panel;

    private bool isGrounded = false;

    private float healthPoint = 3f;

    public static Hero Instance {get; set;}

    public static bool isDead = false;

    

    Rigidbody2D rb;
    SpriteRenderer sprite;
    Animator animation;
    

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        animation = GetComponent<Animator>();
        Instance = this;
    }

    void FixedUpdate()
    {
        CheckGround();
    }

    void Update()
    {
        if(Input.GetButton("Horizontal"))
            PlayerRun(); 
            State = States.run;

        if(isGrounded && Input.GetButtonDown("Jump"))
            PlayerJump();

        if(!Input.GetButton("Horizontal") && !Input.GetButtonDown("Jump"))
            State = States.idle;

        if(!isGrounded)
            State = States.jump;

        if(healthPoint > 3f)
        {
            healthPoint = 3f;
        }

        for(int i = 0; i < hearts.Length; i++)
        {
            if(i < healthPoint)
            {
                hearts[i].sprite = fullHeart;
            } else 
            {
                hearts[i].sprite = emptyHeart;
            }

            if(i < 5)
            {
                hearts[i].enabled = true;
            } else 
            {
                hearts[i].enabled = false;
            }
        }

        Death();
        
    }

    


    void PlayerRun()
    {
        Vector3 dir = transform.right * Input.GetAxis("Horizontal");

        transform.position = Vector3.MoveTowards(transform.position, transform.position + dir, speed * Time.deltaTime);

        sprite.flipX = dir.x < 0f;

    }

    void PlayerJump()
    {
        rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
    }

    void CheckGround()
    {
        Collider2D[] collider = Physics2D.OverlapCircleAll(transform.position, circleRadius);


        if(isGrounded = collider.Length > 1)
        {
            isGrounded = true;
        }
        
    }

    private States State
    {
        get { return (States)animation.GetInteger("states"); }
        set { animation.SetInteger("state", (int)value); }
    }

    void Death()
    {
        if(healthPoint <= 0f)
        {
            panel.SetActive(true);

            isDead = true;

            Destroy(gameObject);


        }
    }

    public void TakeDamage(int amountOfTakenDamage)
    {
        healthPoint -= amountOfTakenDamage;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == GameObject.FindGameObjectWithTag("Enemy"))
        {
            PlayerJump();
        }
    }
}

public enum States
{
    idle,
    run,
    jump
}