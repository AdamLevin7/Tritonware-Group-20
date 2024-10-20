using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ModifyHealthBar : MonoBehaviour
{
    public TMP_Text healthBarText;
    public int health = 100;

    //public InteractionObjectModel gameObjectModel;

    public bool isDead;
    public string loseScene;

    // Start is called before the first frame update
    void Start()
    {
        healthBarText.text = "Health: " +health;
    }

    // Update is called once per frame
    void Update()
    {
        if(Timer.gameTimer >0.0f){
            
        //test to see if health bar changes
            if(Input.GetKey(KeyCode.H))
            {
                health = health-5;
            }
            if(health <= 0)
            {
                SceneManager.LoadScene(loseScene);
            }
            healthBarText.text = "Health: " + health;
        }
    }
    public void healthDamage(bool state)
    {
        if(state == true)
        {
            health = health-5;
        }
        else if(state == false)
        {
            health = health+5;
        }
        
        
    }
}
