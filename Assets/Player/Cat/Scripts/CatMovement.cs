using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using UnityEngine.UI;

public class CatMovement : MonoBehaviour
{
    public float movementSpeed = 1f;   //Movement Speed of the Player
    public Vector2 movement;           //Movement Axis
    public Rigidbody2D rigidbody;      //Player Rigidbody Component

    public Animator anim;
    public float hf = 0.0f;
    public float vf = 0.0f;

    public GameObject UiObject;

    public GameObject hitEffect;

    void Start()
    {
        rigidbody = this.GetComponent<Rigidbody2D>();
        anim = this.GetComponent<Animator>();

        UiObject.SetActive(false);
    }
    void Update()
    {
        EnemyCollision();
        OnCollision();
        ProcessInputs();
    }

    private void EnemyCollision()
    {

    }

    void OnCollision()
    {
        if (GetComponent<Collider2D>().gameObject.tag == "Enemy")
        {
            UiObject.SetActive(true);
            Debug.Log("rah");
        }
    }

    void OnTriggerExit(Collider other)
    {
        UiObject.SetActive(false);
    }


    void FixedUpdate()
    {
        Move();
    }

    void ProcessInputs()
    {

        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        movement = movement.normalized;

        hf = movement.x > 0.01f ? movement.x : movement.x < -0.01f ? 1 : 0;
        vf = movement.y > 0.01f ? movement.y : movement.y < -0.01f ? 1 : 0;
        if (movement.x < -0.01f)
        {
            this.gameObject.transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            this.gameObject.transform.localScale = new Vector3(1, 1, 1);
        }

        anim.SetFloat("Horizontal", hf);
        anim.SetFloat("Vertical", movement.y);
        anim.SetFloat("Speed", vf);
    }

    void Move()
    {
        rigidbody.MovePosition(rigidbody.position + movement * movementSpeed * Time.fixedDeltaTime);
    }
}

