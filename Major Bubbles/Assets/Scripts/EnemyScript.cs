using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public PlayerScript playerScript;
    public int health;
    public int damage;
    public Rigidbody2D rb;
    public int speed;
    public GameObject enemyBullet;
    public float fireRate;
    public float fireCooldown;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        fireRate = Random.Range(0.25f, 1);
        fireCooldown = 1 / fireRate;
    }

    // Update is called once per frame
    void Update()
    {
        fireCooldown -=Time.deltaTime;

        if ( fireCooldown <= 0 )
        {
            Instantiate(enemyBullet, this.transform.position, Quaternion.identity);
            fireCooldown = 1 / fireRate;
        }
        if(health <= 0)
        {
            Destroy(this.gameObject);
        }
    }
    private void FixedUpdate()
    {
        rb.velocity = transform.right * -speed;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerScript>().health = collision.gameObject.GetComponent<PlayerScript>().health-damage;
            Destroy(this.gameObject);
        }
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("EnemyCatcher"))
        {
            Destroy(this.gameObject);
        }
    }
}
