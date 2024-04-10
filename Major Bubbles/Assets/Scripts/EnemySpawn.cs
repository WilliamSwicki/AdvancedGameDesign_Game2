using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public GameObject enemy;
    public Timer timer;
    public float spwanCooldown;
    public float spwanTimer;
    public int spwanAmt;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(timer.minute>=1)
        {
            spwanAmt = timer.minute +1;
        }

        spwanTimer -= Time.deltaTime;
        if ( spwanTimer <= 0 )
        {
            for ( int i = 0; i < spwanAmt; i++)
            {
                Instantiate(enemy,new Vector3(this.transform.position.x+Random.Range(0,5),this.transform.position.y+Random.Range(-4,5)),Quaternion.identity);
            }
            spwanTimer = spwanCooldown;
        }

    }
}
