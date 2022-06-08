using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform player;
    public float moveSpeed = 5;
    public float aggression = 1; // Aggression impacts how much the enemy wants to move in the players direction and how far the enemy can detect the player
    public float scale = 1; // Scale multiplies every calculation that requires distance so that it can be adjusted to smaller or larger scales

    private Rigidbody2D rb;
    private Vector2 movement;

    private Dictionary<float, float> choices; // Choices keeps track of how much enemy wants to move in each direction {angle, weight}
    // ChoicesTemplate gets copied onto choices each update
    private Dictionary<float, float> choicesTemplate = new Dictionary<float, float>()
    {
        {0f, 0f},
        {45f, 0f},
        {90f, 0f},
        {135f, 0f},
        {180f, 0f},
        {225f, 0f},
        {270f, 0f},
        {315f, 0f}
    };

    private float previousDirection;

    // Start is called before the first frame update
    void Start()
    {
        previousDirection = (float)(Random.Range(0, 8) * 45); // At the start previous direction gets set to something random, impacting starting direction
        choicesTemplate[(float)(Random.Range(0, 8) * 45)] += 1f; // For added randomness, each enemy has a favorite direction that has a slight bonus

        rb = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        choices = new Dictionary<float, float>(choicesTemplate); // Resets choices

        Vector3 direction = player.position - transform.position; // Subtracts each value in player by the corresponding value in transform
        float playerDistance = Vector3.Distance(transform.position, player.position) / scale;
        direction.Normalize(); // Adjusts the x and y values so that the length of the hypotenuse equal to 1

        if (playerDistance < 5 + aggression) { // Weight is added to the players direction based on distance and aggression
            UpdateChoices(RoundAngle(DegAngle(direction.y, direction.x)), (8f - playerDistance + aggression), 0.75f);
        }

        UpdateChoices(previousDirection, 2f, 0f); // Enemy prefers to move in the direction that they last moved in
        UpdateChoices((float)(Random.Range(0, 8) * 45), 2f, 0.5f); // For extra randomness weight is added to a random direction

        float targetAngle = 0f;
		float targetValue = 0f;
		
		foreach (float angle in new float[] {0f, 45f, 90f, 135f, 180f, 225f, 270f, 315f})
		{
            RaycastHit2D hit = Physics2D.Raycast(rb.position, new Vector2(Mathf.Cos(angle * Mathf.PI/180),  Mathf.Sin(angle * Mathf.PI/180)), 1.5f * scale, 3); // Raycasts 1.5 units foward for objects in the collision layer (3)
            if (hit.collider != null) { // Doesn't like to move towards walls
                UpdateChoices(angle, -4f, 0.25f);
            }

			if (choices[angle] > targetValue) { // Finds which angle has the most weight
				targetAngle = angle;
				targetValue = choices[angle];
			}
		}

        // Working with decimals is hard so I use degrees, but these have to be converted into radians
        direction.x = Mathf.Cos(targetAngle * Mathf.PI/180);
        direction.y = Mathf.Sin(targetAngle * Mathf.PI/180);

        rb.rotation = targetAngle; 

        previousDirection = targetAngle;
        movement = direction;
    }
    
    void FixedUpdate()
    {
        MoveEnemy(movement); // Actually moves the enemy
    }

    void MoveEnemy(Vector2 _direction) // Script for moving enemy
    {
        rb.MovePosition((Vector2)transform.position + (_direction * moveSpeed * Time.deltaTime)); // Direction has a length of one, which is multiplied by movespeed. deltaTime makes the speed consistent with differing framerates
    }

    float RoundAngle(float _direction) // Rounds an angle to the nearest angle divisible by 45
    {
        return Mathf.Round(_direction/45) * 45f;
    }

    float DegAngle(float _y, float _x) // Coverts sin and cos into degrees
    {
        return Mathf.Abs(Mathf.Atan2(_y, _x) * Mathf.Rad2Deg + 360f) % 360;
    }

    void UpdateChoices(float _angle, float _weight, float _adjacentMultiplier) // Used to update the choice dictionary
    { // Adds _weight * _adjacentMultiplier to nearby angles
        choices[(315f + _angle) % 360f] += _weight*_adjacentMultiplier;
        choices[_angle % 360] += _weight;
        choices[(405f + _angle) % 360f] += _weight*_adjacentMultiplier;
    }
}
