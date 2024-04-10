using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{
    //player movement
    public int speed;
    public int runSpeed;
    Vector2 dir;
    float h, v;
    public bool isRunning;

    //health
    public float health;
    public float MaxHealth;
    public Image healthBar;
    public float healthBarAmt;
    public bool hasDied;

    //support buddy
    public bool isSupportBuddy;
    public GameObject supportBuddy;
    public Image supportBuddyHealthBar;

    //shooting
    public Transform firingPoint;
    public GameObject bullet;
    public GameObject gernade;
    public int gernadeCount = 5;
    public bool isShooting;
    public float fireRate;
    public float fireCooldown;
    //killstreaks
    public KillstreakScript killstreak;
    //aiming
    public Vector2 mousePos;
    public Vector2 mousePosRotation;
    public Vector3 playerPos;
    public Vector3 screenPos;
    public Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        fireCooldown = 1 / fireRate;
        health = MaxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        //health bar
        healthBarAmt = health / MaxHealth;
        healthBar.fillAmount = healthBarAmt;

        //aiming shot
        mousePosRotation = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        playerPos = this.transform.position;
        screenPos = cam.WorldToScreenPoint(playerPos);
        float angle = Mathf.Atan2(mousePosRotation.y - transform.position.y, mousePosRotation.x - transform.position.x) * Mathf.Rad2Deg - 90f;
        firingPoint.transform.rotation = Quaternion.Euler(0, 0, angle);
        
        //fire rate cooldown
        fireCooldown -= Time.deltaTime;
        
        if(isShooting)
        {
            if (fireCooldown <= 0)
            {
                Instantiate(bullet, firingPoint.position, firingPoint.rotation);
                fireCooldown = 1/fireRate;
            }
        }
        //death stuff
        if(health<=0 && hasDied)
        {
            if(isSupportBuddy)
            {
                supportBuddy.SetActive(false);
            }
            if(!isSupportBuddy)
            {
                SceneManager.LoadScene(0);
            }

        }
        else if(health<=0 && !hasDied)
        {
            health = MaxHealth;
            supportBuddy.SetActive(true);
            supportBuddyHealthBar.enabled= true;
            hasDied = true;
        }
    }
    private void FixedUpdate()
    {
        if(!isRunning)
        {
            transform.Translate(dir * Time.fixedDeltaTime * speed);
        }
        if(isRunning)
        {
            transform.Translate(dir * Time.fixedDeltaTime * runSpeed);
        }
    }

    //controll inputs
    public void MovePlayer(InputAction.CallbackContext context)
    {
        dir = context.ReadValue<Vector2>();
        h = dir.x;
        v = dir.y;
    }
    public void Speed(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            isRunning = true;
        }
        else
        {
            isRunning = false;
        }
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
    public void Gernade(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            if(gernadeCount > 0)
            {
                Instantiate(gernade, firingPoint.position, firingPoint.rotation);
                gernadeCount--;
            }
        }
        
    }
    public void Killstreak1(InputAction.CallbackContext context)
    {
        killstreak.UseReload();
    }
    public void Killstreak2(InputAction.CallbackContext context)
    {
        killstreak.UseMachinegun();
    }
    public void Killstreak3(InputAction.CallbackContext context)
    {
        killstreak.UseNuke();
    }
}
