using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPlayer : MonoBehaviour
{
    public Transform player;
    private Rigidbody2D rb;
    public float moveSpeed = 5;
    private Vector2 movement;

    public int maxHealth = 4;
    public int currentenemyHealth;
    public EnemyHealth enemyhealthBar;

    public GameObject testEnemyPrefab;
    GameObject testEnemyPrefabClone;


    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();

        currentenemyHealth = maxHealth;
        enemyhealthBar.SetEnemyMaxHealth(maxHealth);
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
        EnemySpawn();
    }

    void MoveEnemy(Vector2 direction)
    {
        rb.MovePosition((Vector2)transform.position + (direction * moveSpeed * Time.deltaTime));
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Cannonball")
        {
            TakeDamage(1);
        }
    }

    void OnCollisionEnter2D(Collision other)
    {
        if (other.gameObject.tag == "Cat")
        {
            
        }
    }

    void TakeDamage(int damage)
    {
        currentenemyHealth -= damage;

        enemyhealthBar.SetEnemyHealth(currentenemyHealth);
    }
    
    void EnemySpawn()
    {
        if (currentenemyHealth == 0)
        {
            testEnemyPrefabClone = Instantiate(testEnemyPrefab, new Vector3(Random.Range(-5f, 5), Random.Range(-6f, 6f), 0), Quaternion.identity) as GameObject;
            Destroy(testEnemyPrefab);
        }
    }
}
