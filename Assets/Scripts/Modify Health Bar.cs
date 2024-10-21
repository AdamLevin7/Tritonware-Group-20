using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ModifyHealthBar : MonoBehaviour
{
    public int health = 100;
    public Image HealthBar; // Assign this to the Health UI Image in the Inspector
    public string loseScene;

    void Update()
    {
        if (health <= 0)
        {
            LoseGame();
        }
        UpdateHealthBar();
    }

    public void healthDamage(bool state)
    {
        if (state)
        {
            health -= 20;
            Debug.Log($"Damaged health by 20. Current health: {health}");
        }
        else
        {
            health += 20;
            Debug.Log($"Restored health by 20. Current health: {health}");
        }
        UpdateHealthBar();
    }

    private void UpdateHealthBar()
    {
        if (HealthBar != null)
        {
            HealthBar.fillAmount = health / 100f; // Ensure fill amount is based on current health
        }
    }

    private void LoseGame()
    {
        SceneManager.LoadScene(loseScene);
    }
}
