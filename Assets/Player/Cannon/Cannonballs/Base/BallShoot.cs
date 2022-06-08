using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallShoot : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float projectileSpeed = 50f;
    public float shotDelay;
    public float fireRate = 0.5f;
    private float nextFire = 0.0f;

    //card types
    public bool shootBase;
    public bool multiShot;

    void Start()
    {
        shootBase = true;
        multiShot = false;
    }

    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;

            ShootBase();

            if (multiShot == true)
            {
                MultiShot();
            }
        }

    }

    void ShootBase()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * projectileSpeed, ForceMode2D.Impulse);
    }

    void MultiShot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * projectileSpeed, ForceMode2D.Impulse);
    }
}
