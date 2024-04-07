using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{
    //player movement
    public int speed;
    Vector2 dir;
    float h, v;

    //shooting
    public GameObject bullet;
    public bool isShooting;
    public float fireRate;
    public float fireCooldown;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        fireCooldown -= Time.deltaTime;
        
        if(isShooting)
        {
            if (fireCooldown <= 0)
            {
                Instantiate(bullet, new Vector3(this.transform.position.x + 1.0f, this.transform.position.y, 0), Quaternion.identity);
                fireCooldown = fireRate;
            }
        }
    }
    private void FixedUpdate()
    {
        transform.Translate(dir * Time.fixedDeltaTime * speed);
    }
    public void MovePlayer(InputAction.CallbackContext context)
    {
        dir = context.ReadValue<Vector2>();
        h = dir.x;
        v = dir.y;
    }
    public void Shoot(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            isShooting = true;
        }
        else
        {
            isShooting = false;
        }
    }
}
