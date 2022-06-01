using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewCatMovement : MonoBehaviour
{
    public Rigidbody2D body;
    public SpriteRenderer spriteRenderer;
    

    // Lists of Different Directions
    public List<Sprite> nSprites;
    public List<Sprite> neSprites;
    public List<Sprite> eSprites;
    public List<Sprite> seSprites;
    public List<Sprite> sSprites;

    public float walkSpeed;
    public float frameRate;

    float idleTime;

    Vector2 direction;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // get direction
        direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;

        // move based on direction
        body.velocity = direction * walkSpeed;
        
        // handle direction
        HandleSpriteFlip();


        // handle direction
        HandleSpriteFlip();
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
            // doing nothing
        }
    }

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
}
