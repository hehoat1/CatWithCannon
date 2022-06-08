using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewCatMovement : MonoBehaviour
{
    public Rigidbody2D body;
    public SpriteRenderer spriteRenderer;
    

    // Lists of Different Directions
    public List<Sprite> walkingAnimation;

    public float walkSpeed;
    public float frameRate;
    public float rotationSpeed;

    float idleTime;

    Vector2 directionWithSpeed;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Rotate();
        Move();
        Animate();
    }


    // Functions
    void Move()
    {
        // get direction
        Vector2 direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;

        // move based on direction
        directionWithSpeed = direction * walkSpeed;
        body.velocity = directionWithSpeed;
    }

    void Rotate() //simplify?
        {
            if (directionWithSpeed.x < 0 && directionWithSpeed.y > 0)
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0.0f, 0.0f, 45.0f), Time.deltaTime * rotationSpeed);
            }

            else if (directionWithSpeed.x < 0 && directionWithSpeed.y == 0)
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0.0f, 0.0f, 90.0f), Time.deltaTime * rotationSpeed);
            }

            else if (directionWithSpeed.x < 0 && directionWithSpeed.y < 0)
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0.0f, 0.0f, 135.0f), Time.deltaTime * rotationSpeed);
            }

            else if (directionWithSpeed.x == 0 && directionWithSpeed.y < 0)
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0.0f, 0.0f, 180.0f), Time.deltaTime * rotationSpeed);
            }

            else if (directionWithSpeed.x > 0 && directionWithSpeed.y < 0)
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0.0f, 0.0f, 225.0f), Time.deltaTime * rotationSpeed);
            }

            else if (directionWithSpeed.x > 0 && directionWithSpeed.y == 0)
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0.0f, 0.0f, 270.0f), Time.deltaTime * rotationSpeed);
            }

            else if (directionWithSpeed.x > 0 && directionWithSpeed.y > 0)
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0.0f, 0.0f, 315.0f), Time.deltaTime * rotationSpeed);
            }

            else if (directionWithSpeed.x == 0 && directionWithSpeed.y > 0) 
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0.0f, 0.0f, 0.0f), Time.deltaTime * rotationSpeed);
            }
        }

    // List<Sprite> GetSpriteDirection()
    // {
    //     List<Sprite> selectedSprites = null;

    //     if (direction.y > 0) // north
    //     {
    //         if (Mathf.Abs(direction.x) > 0)
    //         {
    //             selectedSprites = neSprites;
    //         }

    //         else
    //         {
    //             selectedSprites = nSprites;
    //         }
    //     }
        
    //     else if (direction.y < 0) // south
    //     {
    //         if (Mathf.Abs(direction.x) > 0)
    //         {
    //             selectedSprites = seSprites;
    //         }

    //         else
    //         {
    //             selectedSprites = sSprites;
    //         }
    //     } 
        
    //     else // neutral
    //     {
    //         if (Mathf.Abs(direction.x) > 0)
    //         {
    //             selectedSprites = eSprites;
    //         }
    //     }

    //     return selectedSprites;


    // }
    void Animate()
    {
        if (!(directionWithSpeed.x == 0 && directionWithSpeed.y == 0)) // actually moving? how does this work???
        {
            float playTime = Time.time - idleTime;
            int totalFrames = (int)(playTime * frameRate);
            int frame = totalFrames % walkingAnimation.Count; 

            spriteRenderer.sprite = walkingAnimation[frame];
        }

        else
        {
            spriteRenderer.sprite = walkingAnimation[2];
        }
    }
}
