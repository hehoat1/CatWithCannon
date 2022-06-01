using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CatMovement : MonoBehaviour
{
    public float movementSpeed = 1f;   //Movement Speed of the Player
    public Vector2 movement;           //Movement Axis
    public Rigidbody2D body;      //Player Rigidbody Component

    public Animator anim;
    public float hf = 0.0f;
    public float vf = 0.0f;

    public int maxHealth = 9;
    public int currentHealth;
    public HealthBar healthBar;

    public GameObject bullet;

    public GameObject hitEffect;

    public GameObject UiObject;

    void Start()
    {
        body = this.GetComponent<Rigidbody2D>();
        anim = this.GetComponent<Animator>();

        UiObject.SetActive(false);

        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);

        Physics2D.IgnoreCollision(bullet.GetComponent<Collider2D>(), GetComponent<Collider2D>());
    }

    void Update()
    {
        AnimationInputs();
        ProcessInputs();
        DeathDetection();
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            TakeDamage(1);
            Debug.Log("rah");
        }
    }

    void TakeDamage(int damage)
    {
        currentHealth -= damage;
       
        GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
        Destroy(effect, 0.2f);

        healthBar.SetHealth(currentHealth);
    }

    void DeathDetection()
    {
        if (currentHealth == 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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

    void AnimationInputs()
    {
 
    }

    void Move()
    {
        body.MovePosition(body.position + movement * movementSpeed * Time.fixedDeltaTime);
    }
}

