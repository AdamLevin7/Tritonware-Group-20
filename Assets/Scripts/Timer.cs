using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
    // Start is called before the first frame update
    public float startTime = 60.0f;
    private float time;
    public static float gameTimer;
    public Image StopWatch;

    public string loseScene;

    public GameObject TimerObject;
    public GameObject healthBar;
    void Start (){
        time = startTime;
        healthBar = GameObject.FindGameObjectWithTag("Health");
    }
    
    // Update is called once per frame
    void Update()
    {
        gameTimer= time;
       time -= Time.deltaTime;
       if(time <= 0.0f){
        healthBar.GetComponent<ModifyHealthBar>().healthDamage(true);
        Destroy(this.gameObject);
       }
       StopWatch.fillAmount -= 1.0f / startTime * Time.deltaTime;
    }
    void Awake(){
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Barn Timer");
       
        if (objs.Length > 1){
            Destroy(this.gameObject);
        } 
    }
}
