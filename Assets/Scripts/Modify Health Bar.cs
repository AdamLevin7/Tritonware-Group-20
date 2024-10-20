using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Events;

public class ModifyHealthBar : MonoBehaviour
{
    public int health = 100;

    //public InteractionObjectModel gameObjectModel;
    public string loseScene;
    public bool isDead;
    public GameObject HealthObject;
    public Image HealthBar;
    

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(health <= 0.0f){
            lose();
            Destroy(this.gameObject);
       }
       HealthBar.fillAmount = (float)(health/100f);
       
        
    }
    public void healthDamage(bool state)
    {
        if(state == true)
        {
            health = health-20;
        }
        else if(state == false)
        {
            health = health+20;
        }
        
        
    }
    void lose(){
        SceneManager.LoadScene(loseScene);
    }
}
