using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventSystem : MonoBehaviour
{
    public GameObject AGH;
    public float time;

    public GameObject timer;
    private bool AGHEvent = false;
    // Start is called before the first frame update
    void Start()
    {
        time = Random.Range(2.0f, 4.0f);
        AGH.SetActive(false);
        timer.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;
        if (time < 0 && !AGHEvent){
            AHH();
            AGHEvent = true;
        }
    }

    void AHH(){
        AGH.SetActive(true);
       // GameObject newTimer = Instantiate(timer, AGH.transform.position, AGH.transform.rotation);
        //newTimer.SetActive(true);
        timer.SetActive(true);
    }
}
