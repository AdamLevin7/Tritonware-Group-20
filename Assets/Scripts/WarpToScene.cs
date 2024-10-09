using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WarpToScene : MonoBehaviour
{
    // Start is called before the first frame update
    private bool canEnter = false;
    public string sceneToEnter;

    // Update is called once per frame
    void Update()
    {
        if(canEnter && Input.GetKey(KeyCode.Space)){
            SceneManager.LoadScene(sceneToEnter);
        }
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player"){
            canEnter = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other) {
        if (other.tag == "Player"){
            canEnter = false;
        }
    }
}
