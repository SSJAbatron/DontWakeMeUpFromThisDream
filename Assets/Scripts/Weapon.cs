using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform FirePoint;
    public GameObject bulletPrefab;
    public AudioSource audioSource;
    public float bulletForce = 20f;

    // Update is called once per frame
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }


    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, FirePoint.position, FirePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(FirePoint.up * bulletForce, ForceMode2D.Impulse);
        audioSource.Play();
    }
}
