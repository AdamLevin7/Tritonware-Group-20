using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EventSystem : MonoBehaviour
{
    public GameObject AGH;
    public GameObject canvas;
    public float barnTime;
    public float animalTime;

    private bool animalEvent;

    public GameObject AGHTimer;
    private bool AGHEvent = false;
    public string overworldScene;
    private bool overWorld = true;
    // Start is called before the first frame update
    void Start()
    {
        barnTime = Random.Range(2.0f, 4.0f);
        AGH.SetActive(false);
        AGHTimer.SetActive(false);
        
        animalTime = Random.Range(2.0f, 4.0f);
    }

    // Update is called once per frame
    void Update()
    {
        barnTime -= Time.deltaTime;
        animalTime -= Time.deltaTime;
        if (barnTime < 0 && !AGHEvent){
            AHH();
            AGHEvent = true;
            barnTime = Random.Range(2.0f, 4.0f);
        }

        if (animalTime < 0 && !animalEvent){
            animal();
            animalEvent = true;
            animalTime = Random.Range(5.0f, 10.0f);
        }

        if(SceneManager.GetActiveScene().name != overworldScene && overWorld == true){
            AGH.GetComponent<Renderer>().enabled = false;
            if(AGHTimer != null) AGHTimer.GetComponent<Renderer>().enabled = false;
            overWorld = false;
        }
        else if(SceneManager.GetActiveScene().name == overworldScene && overWorld == false){
            AGH.GetComponent<Renderer>().enabled = true;
            if(AGHTimer != null) AGHTimer.GetComponent<Renderer>().enabled = true;
            overWorld = true;
        }
    }

    void AHH(){
        AGH.SetActive(true);
        AGHTimer.SetActive(true);
       // GameObject newTimer = Instantiate(timer, AGH.transform.position, AGH.transform.rotation);
        //newTimer.SetActive(true);
        //AGHtimer.SetActive(true);
        Instantiate(AGHTimer, AGH.transform);
        AGHTimer.transform.position = AGH.transform.position + 
            new Vector3(0.2f, 0.2f, 0);
    }
    void animal(){

    }
    void Awake(){
        DontDestroyOnLoad(this.gameObject);
        DontDestroyOnLoad(AGH);
        DontDestroyOnLoad(AGHTimer);
        DontDestroyOnLoad(canvas);
    }
}
