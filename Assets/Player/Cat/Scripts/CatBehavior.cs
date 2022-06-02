using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CatBehavior : MonoBehaviour
{
    // Body and Spriterender
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
    Vector2 direction;
    int currentHealth;

    int maxHealth = 9;


    // Unity Functions
    void Start()
    {
        UiObject.SetActive(false);

        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);

        Physics2D.IgnoreCollision(bullet.GetComponent<Collider2D>(), GetComponent<Collider2D>()); // dont think 'bullet' will work
    }

    void Update()
    {
        DeathDetection();
        Animate();
        HandleSpriteFlip(); // handle direction flip
    }
    void FixedUpdate()
    {
        // get direction
        direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;

        // move based on direction
        body.velocity = direction * walkSpeed;
    }

    // Non-Unity Functions

    // Animation
    void HandleSpriteFlip()
    {

        if(!spriteRenderer.flipX && direction.x < 0)

        {
            spriteRenderer.flipX = true;
        }
        

        else if(spriteRenderer.flipX && direction.x > 0)

        {
            spriteRenderer.flipX = false;
        }   
    }

    List<Sprite> GetSpriteDirection()
    {
        List<Sprite> selectedSprites = null;

        if (direction.y > 0) // north
        {
            if (Mathf.Abs(direction.x) > 0)
            {
                selectedSprites = neSprites;
            }

            else
            {
                selectedSprites = nSprites;
            }
        }
        
        else if (direction.y < 0) // south
        {
            if (Mathf.Abs(direction.x) > 0)
            {
                selectedSprites = seSprites;
            }

            else
            {
                selectedSprites = sSprites;
            }
        } 
        
        else // neutral
        {
            if (Mathf.Abs(direction.x) > 0)
            {
                selectedSprites = eSprites;
            }
        }

        return selectedSprites;
    }

    void Animate()
    {
        List<Sprite> directionSprites = GetSpriteDirection();

        if (directionSprites != null)
        {
            float playTime = Time.time - idleTime;
            int totalFrames = (int)(playTime * frameRate);
            int frame = totalFrames % directionSprites.Count; 

            spriteRenderer.sprite = directionSprites[frame];
        }

        else
        {
            idleTime = Time.time;
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