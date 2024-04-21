using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D rb;
    public Transform player;
    public float movementSpeed = 3f;
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletForce;
    Vector2 movement;
    public float distanceToStop;
    public float distanceToShoot;
    public float fireRate;
    private float timeToFire = 0f;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        RotateTowardsPlayer();
        if(Vector2.Distance(player.position,transform.position) <= distanceToShoot) 
        {
            Shoot();
        }
    }

    private void FixedUpdate()
    {
        if (player != null)
        {
            if (Vector2.Distance(player.position, transform.position) >= distanceToStop)
            {
                MoveEnemy(movement);
            }
            else
                movement = Vector2.zero;
        }
           

    }

    private void Shoot()
    {
        if (timeToFire <= 0f)
        {
            timeToFire = fireRate;
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
            //audioSource.Play()
        }
        else
        {
            timeToFire-= Time.deltaTime;
        }
    }


    private void RotateTowardsPlayer()
    {
        Vector3 enemyDirection = player.position - transform.position;
        float enemyAngle = Mathf.Atan2(enemyDirection.y, enemyDirection.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = enemyAngle;
        enemyDirection.Normalize();
        movement = enemyDirection;

    }
    private void MoveEnemy(Vector2 direction)
    {
       rb.MovePosition((Vector2)transform.position + direction * movementSpeed * Time.fixedDeltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Bullet"))
        {
            rb.AddForce(transform.up * bulletForce, ForceMode2D.Impulse);
            Destroy(this.gameObject);
            WakeUpMeter.instance.UpdateDontWakeUpMeter(30);
            player.gameObject.GetComponent<Player>().score += 10;
        }
    }

}
