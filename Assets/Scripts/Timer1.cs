using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Timer1 : MonoBehaviour
{
    public float startTime = 60.0f;
    private float time;
    public static float gameTimer;
    public Image StopWatch; // Assign this to the Stopwatch UI Image in the Inspector

    void Start()
    {
        time = startTime;
    }

    void Update()
    {
        time -= Time.deltaTime;
        gameTimer = time;

        // Update the stopwatch UI fill amount
        if (StopWatch != null)
        {
            float fillAmount = Mathf.Clamp(time / startTime, 0, 1);
            StopWatch.fillAmount = fillAmount;
        }

        // Check if the timer has finished
        if (time <= 0.0f)
        {
            Debug.Log("Timer Completed"); // This can remain for debugging
            if(this.gameObject.CompareTag("GameTimer"))
            {
                SceneManager.LoadScene("You Win");
            }
            Destroy(this.gameObject); // Destroy the timer object
        }
    }
}
