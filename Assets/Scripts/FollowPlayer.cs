using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform player;
    private Rigidbody2D rb;
    public float moveSpeed = 5;
    private Vector2 movement;


    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Vector3 direction = player.position - transform.position;
        rb.rotation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        movement = direction;
    }

    void FixedUpdate()
    {
        MoveEnemy(movement);
    }

    void MoveEnemy(Vector2 direction)
    {
        rb.MovePosition((Vector2)transform.position + (direction * moveSpeed * Time.deltaTime));
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(GetComponent<Collider2D>().gameObject.tag == "cat sprite")
        {
            
        }
    }
        
}
