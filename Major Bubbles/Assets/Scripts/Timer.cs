using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    public float time;
    public int minute;
    public TMP_Text text;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time>=60)
        {
            minute++;
            time = 0;
        }
        if (Mathf.Round(time) < 10)
        {
            text.text = "Time: " + minute + ":0" + Mathf.Round(time);
        }
        else
        {
            text.text = "Time: " + minute +":"+ Mathf.Round(time);
        }
    }
}
