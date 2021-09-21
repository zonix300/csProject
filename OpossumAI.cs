using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpossumAI : MonoBehaviour
{

    [SerializeField] private float speed;

    [SerializeField] private int distanceOfPatrol; 
    [SerializeField] private Transform pointOfPatrol;

    public static OpossumAI Instance {get; set;}


    Animator animation;
    SpriteRenderer sprite;
    Rigidbody2D rb;
    BoxCollider2D bc;

    private bool moveRight;
    
    void Start()
    {
        animation = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        bc = GetComponent<BoxCollider2D>();
        Instance = this;
    }

    
    void Update()
    {
        if(Vector2.Distance(transform.position, pointOfPatrol.position) < distanceOfPatrol)
        {
            Patrol();
        }
    }

    void Patrol()
    {
        if(transform.position.x + 0.09f > pointOfPatrol.position.x + distanceOfPatrol)
        {
            moveRight = false;

        } else if(transform.position.x - 0.09f < pointOfPatrol.position.x - distanceOfPatrol)
        {
            moveRight = true;
        }

        if(moveRight)
        {
            transform.position = new Vector2(transform.position.x + speed * Time.deltaTime, transform.position.y);
            sprite.flipX = true;
        } else 
        {
            transform.position = new Vector2(transform.position.x - speed * Time.deltaTime, transform.position.y);
            sprite.flipX = false;
        }
    }

    IEnumerator Die()
    {
        yield return new WaitForSeconds(0.65f);
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject == Hero.Instance.gameObject)
        {
            animation.SetTrigger("Death");

            speed = 0f;
            bc.enabled = false;
            rb.gravityScale = 0f;

            StartCoroutine("Die");
        }

    }


}
