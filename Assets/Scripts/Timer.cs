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

    public Image StopWatch;

    public string loseScene;

    public GameObject TimerObject;
    void Start (){
        time = startTime;
    }
    
    // Update is called once per frame
    void Update()
    {
       time -= Time.deltaTime;
       if(time <= 0.0f){
        lose();
       }
       StopWatch.fillAmount -= 1.0f / startTime * Time.deltaTime;
    }
    void lose(){
        SceneManager.LoadScene(loseScene);
    }
    void Awake(){
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Barn Timer");
       
        if (objs.Length > 1){
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(TimerObject);
    }
}
