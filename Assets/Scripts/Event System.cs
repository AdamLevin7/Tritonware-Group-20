using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EventSystem : MonoBehaviour
{
    public GameObject AGH;
    public GameObject canvas;
    public float time;

    public GameObject AGHtimer;
    private bool AGHEvent = false;
    public string overworldScene;
    private bool overWorld = true;
    // Start is called before the first frame update
    void Start()
    {
        time = Random.Range(2.0f, 4.0f);
        AGH.SetActive(false);
        AGHtimer.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;
        if (time < 0 && !AGHEvent){
            AHH();
            AGHEvent = true;
        }

        if(SceneManager.GetActiveScene().name != overworldScene && overWorld == true){
            AGH.GetComponent<Renderer>().enabled = false;
            AGHtimer.GetComponent<Renderer>().enabled = false;
            overWorld = false;
        }
        else if(SceneManager.GetActiveScene().name == overworldScene && overWorld == false){
            AGH.GetComponent<Renderer>().enabled = true;
            AGHtimer.GetComponent<Renderer>().enabled = true;
            overWorld = true;
        }
    }

    void AHH(){
        AGH.SetActive(true);
       // GameObject newTimer = Instantiate(timer, AGH.transform.position, AGH.transform.rotation);
        //newTimer.SetActive(true);
        AGHtimer.SetActive(true);
    }
    void Awake(){
        DontDestroyOnLoad(this.gameObject);
        DontDestroyOnLoad(AGH);
        DontDestroyOnLoad(AGHtimer);
        DontDestroyOnLoad(canvas);
    }
}
