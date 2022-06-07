using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CatBehavior : MonoBehaviour
{
    // Cat Body Transformation References (Unity)
    public Rigidbody2D body;    
    public SpriteRenderer spriteRenderer;
    
    // Changeable values
    public float walkSpeed;
    public float frameRate;

    // Lists of Different Directions
    public List<Sprite> nSprites;
    public List<Sprite> neSprites;
    public List<Sprite> eSprites;
    public List<Sprite> seSprites;
    public List<Sprite> sSprites;

    // Other references
    public HealthBar healthBar;

    public GameObject bullet;

    public GameObject hitEffect;

    public GameObject UiObject;

    // Private variables
    float idleTime;
    int currentHealth;
    Vector2 directionWithSpeed;
    int maxHealth = 9;


    // Unity Functions
    void Start()
    {
        UiObject.SetActive(false);

        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    void Update()
    {
        DeathDetection();
    }
    void FixedUpdate()
    {
        // get direction
        Vector2 direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;

        // move based on direction
        directionWithSpeed = direction * walkSpeed;
        body.velocity = directionWithSpeed;
        Rotate(directionWithSpeed);
        Animate();
    }

    // Animation

    void Rotate(Vector2 directionWithSpeed)
    {
        if (directionWithSpeed.x < 0 && directionWithSpeed.y > 0)
        {
            transform.rotation = Quaternion.Euler(0.0f, 0.0f, 45.0f);
        }

        else if (directionWithSpeed.x < 0 && directionWithSpeed.y == 0)
        {
            transform.rotation = Quaternion.Euler(0.0f, 0.0f, 90.0f);
        }

        else if (directionWithSpeed.x < 0 && directionWithSpeed.y < 0)
        {
            transform.rotation = Quaternion.Euler(0.0f, 0.0f, 135.0f);
        }

        else if (directionWithSpeed.x == 0 && directionWithSpeed.y < 0)
        {
            transform.rotation = Quaternion.Euler(0.0f, 0.0f, 180.0f);
        }

        else if (directionWithSpeed.x > 0 && directionWithSpeed.y < 0)
        {
            transform.rotation = Quaternion.Euler(0.0f, 0.0f, 225.0f);
        }

        else if (directionWithSpeed.x > 0 && directionWithSpeed.y == 0)
        {
            transform.rotation = Quaternion.Euler(0.0f, 0.0f, 270.0f);
        }

        else if (directionWithSpeed.x > 0 && directionWithSpeed.y > 0)
        {
            transform.rotation = Quaternion.Euler(0.0f, 0.0f, 315.0f);
        }

        else if (directionWithSpeed.x == 0 && directionWithSpeed.y > 0) 
        {
            transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
        }
    }

    void Animate()
    {
        if (true) // actually moving?
        {
            float playTime = Time.time - idleTime;
            int totalFrames = (int)(playTime * frameRate);
            int frame = totalFrames % nSprites.Count; 

            spriteRenderer.sprite = nSprites[frame];
        }
        else
        {
            spriteRenderer.sprite = nSprites[2];
        }
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
}