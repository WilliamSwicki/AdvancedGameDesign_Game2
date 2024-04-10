using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class KillstreakScript : MonoBehaviour
{
    public PlayerScript playerScript;

    public TMP_Text gerCount;

    public bool reload;
    public float reloadCooldown;
    public float reloadCooldownTimer;
    public Image reloadImage;
    public float reloadImageAmt;

    public bool machinegun;
    public float machinegunCooldown;
    public float machinegunCooldownTimer;
    public Image machinegunImage;
    public float machinegunImageAmt;

    public bool nuke;
    public float nukeCooldown;
    public float nukeCooldownTimer;
    public Image nukeImage;
    public float nukeImageAmt;
    public GameObject nukeSpwanPoint;
    public GameObject nukeObject;

    // Start is called before the first frame update
    void Start()
    {
        reloadCooldownTimer = reloadCooldown = 15f;
        machinegunCooldownTimer = machinegunCooldown = 30f;
        nukeCooldownTimer = nukeCooldown = 60f;
    }

    // Update is called once per frame
    void Update()
    {
        //visual rep of cooldowns
        reloadImageAmt = reloadCooldownTimer / reloadCooldown;
        reloadImage.fillAmount = reloadImageAmt;
        
        machinegunImageAmt = machinegunCooldownTimer / machinegunCooldown;
        machinegunImage.fillAmount = machinegunImageAmt;

        nukeImageAmt = nukeCooldownTimer / nukeCooldown;
        nukeImage.fillAmount = nukeImageAmt;
        
        reloadCooldownTimer -= Time.deltaTime;
        machinegunCooldownTimer -= Time.deltaTime;
        nukeCooldownTimer -= Time.deltaTime;
        
        //check if timer is up
        if(reloadCooldownTimer >0)
        {
            reload = false;
        }
        else
        {
            reload = true;
        }

        if(machinegunCooldownTimer > 0)
        {
            machinegun = false;
        }
        else
        {
            machinegun = true;
        }

        if(nukeCooldownTimer > 0) 
        { 
            nuke = false; 
        }
        else 
        { 
            nuke = true; 
        }

        gerCount.text = playerScript.gernadeCount.ToString();
    }
    public void UseReload()
    {
        if (reloadCooldownTimer <= 0)
        {
            playerScript.gernadeCount += 2;
            playerScript.health += 10;
            if (playerScript.health > 100)
            {
                playerScript.health = 100;
            }
            if (playerScript.gernadeCount > 10)
            {
                playerScript.gernadeCount = 10;
            }
            reloadCooldownTimer = reloadCooldown;
        }
        
    }
    public void UseMachinegun()
    {
        if (machinegunCooldownTimer <= 0)
        {
            StartCoroutine(MachinegunTimer());
        }
    }
    public void UseNuke()
    {
        if (nukeCooldownTimer <= 0)
        {
            Instantiate(nukeObject, nukeSpwanPoint.transform.position, Quaternion.identity);
            nukeCooldownTimer = nukeCooldown;
        }
    }

    public IEnumerator MachinegunTimer()
    {
        playerScript.fireRate = 5;
        yield return new WaitForSeconds(10f);
        playerScript.fireRate = 1;
        machinegunCooldownTimer = machinegunCooldown;
    }
}
