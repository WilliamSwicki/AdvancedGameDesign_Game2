using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    Rigidbody2D rb;
    public int damage;
    public float speed = 30f;
    public bool isGernade;
    public bool isExplosion;
    public bool isEnemyBullet;
    public GameObject explosion;


    // Start is called before the first frame update
    void Start()
    {
        rb= GetComponent<Rigidbody2D>();
        Destroy(gameObject, 2f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        if (isExplosion)
        {
            rb.velocity = Vector2.zero;
        }
        else
        {
            if(isEnemyBullet)
            {
                rb.velocity = transform.right * -speed;
            }
            else
            {
                rb.velocity = transform.up * speed;
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isEnemyBullet)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                collision.gameObject.GetComponent<PlayerScript>().health -= damage;
                Destroy(this.gameObject);
            }
        }
        else
        {
            if (collision.gameObject.CompareTag("Enemy"))
            {
                collision.gameObject.GetComponent<EnemyScript>().health -= damage;
                if (isGernade)
                {
                    Instantiate(explosion, this.gameObject.transform.position, Quaternion.identity);
                }
            }
            if (isExplosion)
            {
                StartCoroutine(ExplosionDecay());
            }
            else
            {
                Destroy(this.gameObject);
            }
        }
    }

    public IEnumerator ExplosionDecay()
    {
        this.gameObject.GetComponent<Collider2D>().enabled = false;
        yield return new WaitForSeconds(0.25f);
        Destroy(this.gameObject);
    }
}
