using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCollide : MonoBehaviour
{
    public GameObject hitEffect;
    public GameObject bullet;
    public GameObject bulletPrefab;
    public bool multiShot;
    public float projectileSpeed;

    void Start()
    {
        Physics2D.IgnoreLayerCollision(10, 13, true);

        projectileSpeed = 10;

        multiShot = false;
    }

    void Update()
    {
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (multiShot == false)
        {
            GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
            Destroy(effect, 0.2f);
            Destroy(gameObject);
        }

        if (multiShot == true)
        {
            multiShot = false;
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.AddForce(transform.right * projectileSpeed, ForceMode2D.Impulse);
        }
    }

}
