using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EventSystem : MonoBehaviour
{
    public GameObject AGH;
    public GameObject canvas;
    public GameObject Animal;
    public float barnTime;
    public float animalTime;

    public bool animalEvent;

    public GameObject timer;
    public List<GameObject> timers = new List<GameObject> {null, null, null};
    public bool AGHEvent = false;
    public string overworldScene;
    private bool overWorld = true;
    // Start is called before the first frame update
    void Start()
    {
        barnTime = Random.Range(2.0f, 4.0f);
        AGH.SetActive(false);
        
        animalTime = Random.Range(2.0f, 4.0f);
    }

    // Update is called once per frame
    void Update()
    {
        barnTime -= Time.deltaTime;
        animalTime -= Time.deltaTime;
        if (barnTime < 0){
            AHH();
            AGHEvent = true;
            barnTime = Random.Range(10.0f, 12.0f);
        }

        if (animalTime < 0){
            animal();
            animalEvent = true;
            animalTime = Random.Range(10.0f, 12.0f);
        }

        if(SceneManager.GetActiveScene().name != overworldScene && overWorld == true){
            AGH.GetComponent<Renderer>().enabled = false;
            if(timers[0] != null) timers[0].GetComponent<Renderer>().enabled = false;
            overWorld = false;
        }
        else if(SceneManager.GetActiveScene().name == overworldScene && overWorld == false){
            AGH.GetComponent<Renderer>().enabled = true;
            if(timers[0] != null) timers[0].GetComponent<Renderer>().enabled = true;
            overWorld = true;
        }
    }

    void AHH(){
        AGH.SetActive(true);
        timers[0].SetActive(true);
       // GameObject newTimer = Instantiate(timer, AGH.transform.position, AGH.transform.rotation);
        //newTimer.SetActive(true);
        //AGHtimer.SetActive(true);
        Instantiate(timers[0], AGH.transform);
        timers[0].transform.position = AGH.transform.position + 
            new Vector3(10.0f, 5.0f, 0);
    }
    void animal(){
        timers[1].SetActive(true);
        Instantiate(timers[1], Animal.transform);
        timers[1].transform.position = Animal.transform.position + 
            new Vector3(10.0f, 5.0f, 0);
    }
    void Awake(){
        DontDestroyOnLoad(this.gameObject);
        DontDestroyOnLoad(AGH);
        DontDestroyOnLoad(timers[0]);
        DontDestroyOnLoad(canvas);
    }
}
