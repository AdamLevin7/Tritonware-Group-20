using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EventSystem : MonoBehaviour
{
    public GameObject AGH;
    public GameObject canvas;
    public GameObject Animal;
    public GameObject Cow;
    public GameObject Chicken;
    public float barnTime;
    public float animalTime;
    public float cowTime;
    public float chickenTime;

    public bool animalEvent;
    public bool cowEvent;
    public bool chickenEvent;

    public List<GameObject> timers = new List<GameObject> {null, null, null, null};
    public bool AGHEvent = false;
    public string overworldScene;
    private bool overWorld = true;
    // Start is called before the first frame update
    void Start()
    {
        barnTime = Random.Range(2.0f, 4.0f);
        AGH.SetActive(false);
        
        animalTime = Random.Range(2.0f, 4.0f);
        
        cowTime = Random.Range(2.0f, 4.0f);
        
        chickenTime = Random.Range(2.0f, 4.0f);
    }

    // Update is called once per frame
    void Update()
    {
        barnTime -= Time.deltaTime;
        animalTime -= Time.deltaTime;
        cowTime -= Time.deltaTime;
        chickenTime -= Time.deltaTime;
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
        if (cowTime < 0){
            cow();
            cowEvent = true;
            cowTime = Random.Range(10.0f, 12.0f);
        }

        if (chickenTime < 0){
            chicken();
            chickenEvent = true;
            chickenTime = Random.Range(10.0f, 12.0f);
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
        timers[0].transform.position = AGH.transform.position + 
            new Vector3(4.0f, 5.0f, 0);
        timers[1].transform.position = Animal.transform.position + 
            new Vector3(-1.0f, 1.0f, 0);
        timers[2].transform.position = Cow.transform.position + 
            new Vector3(-37.0f, 1.0f, 0);
        timers[3].transform.position = Chicken.transform.position + 
            new Vector3(-9.5f, 1.0f, 0);
    }

    void AHH(){
        AGH.SetActive(true);
        timers[0].SetActive(true);
        Instantiate(timers[0], AGH.transform);
        timers[0].transform.position = AGH.transform.position + 
            new Vector3(10.0f, 5.0f, 0);
        timers[0].transform.localScale = new Vector3(0.75f, 0.75f, 0.75f);
    }
    void animal(){
        timers[1].SetActive(true);
        Instantiate(timers[1], Animal.transform);
        timers[1].transform.position = Animal.transform.position + 
            new Vector3(10.0f, 5.0f, 0);
        timers[1].transform.localScale = new Vector3(0.75f, 0.75f, 0.75f);
    }
    void cow(){
        timers[2].SetActive(true);
        Instantiate(timers[2], Cow.transform);
        timers[2].transform.position = Cow.transform.position + 
            new Vector3(10.0f, 5.0f, 0);
        timers[2].transform.localScale = new Vector3(0.75f, 0.75f, 0.75f);
    }
    void chicken(){
        timers[3].SetActive(true);
        Instantiate(timers[3], Chicken.transform);
        timers[3].transform.position = Chicken.transform.position + 
            new Vector3(10.0f, 5.0f, 0);
        timers[3].transform.localScale = new Vector3(0.75f, 0.75f, 0.75f);
    }
    void Awake(){
        DontDestroyOnLoad(this.gameObject);
        DontDestroyOnLoad(AGH);
        DontDestroyOnLoad(timers[0]);
        DontDestroyOnLoad(canvas);
    }
}
