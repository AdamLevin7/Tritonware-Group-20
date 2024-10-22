using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventsDraft : MonoBehaviour
{
    public Dictionary<string, (float time, bool completed)> objectTimers = new Dictionary<string, (float, bool)>();
    public Dictionary<string, GameObject> interactableObjects = new Dictionary<string, GameObject>();
    public Dictionary<string, GameObject> objectTimersUI = new Dictionary<string, GameObject>();

    public GameObject timerPrefab;  // Prefab reference for Timer
    public Canvas uiCanvas;         // Reference to the UI Canvas
    private ModifyHealthBar healthBar; // Reference to ModifyHealthBar

    public List<string> excludedObjectNames = new List<string>(); // Add names in the Inspector

    public float respawnInterval = 3.0f; // Time interval to check for timer respawns
    public float creationChance = 0.7f;   // Probability (0.0 - 1.0) of creating a new timer for an object

    private float respawnTimer = 0f; // Timer to track elapsed time for respawning

    void Start()
    {
        foreach (var interactionObject in FindObjectsOfType<InteractionObjectModel>())
        {
            string objectID = interactionObject.objectID;
            interactableObjects.Add(objectID, interactionObject.gameObject);

            if (!excludedObjectNames.Contains(interactionObject.ObjectName) && !interactionObject.CompareTag("Player"))
            {
                if (Random.value <= creationChance)
                {
                    // Initialize timers with a random initial time and mark them as not completed
                    objectTimers[objectID] = (Random.Range(15.0f, 30.0f), false); 

                    GameObject timerUI = Instantiate(timerPrefab, uiCanvas.transform);
                    timerUI.GetComponent<Timer1>().startTime = objectTimers[objectID].time;
                    objectTimersUI.Add(objectID, timerUI);

                    timerUI.transform.localScale = new Vector3(2.0f, 2.0f, 1); 
                    timerUI.transform.localPosition = new Vector3(4.0f, 5.0f, 0); 
                    interactionObject.SetIsDamaged(true);
                }
            }
            else
            {
                Debug.Log($"Excluding timer for object: {interactionObject.ObjectName}");
            }
        }

        healthBar = GameObject.FindGameObjectWithTag("Health").GetComponent<ModifyHealthBar>();
        if (healthBar == null)
        {
            Debug.LogError("HealthBar not found! Ensure it has the 'Health' tag and the ModifyHealthBar script is attached.");
        }
    }

    void Update()
    {
        List<string> completedObjects = new List<string>();
        List<string> objectIDs = new List<string>(objectTimers.Keys);

        foreach (string objectID in objectIDs)
        {
            var (time, completed) = objectTimers[objectID];
            
            if (!completed)
            {
                time -= Time.deltaTime;

                // Update the timer in the dictionary
                objectTimers[objectID] = (time, completed);

                // When a timer reaches zero, trigger the associated event
                if (time < 0)
                {
                    HandleEvent(objectID);
                    objectTimers[objectID] = (time, true);  // Mark the timer as completed
                    completedObjects.Add(objectID);
                }

                // Move the timer UI to follow the object
                if (interactableObjects.ContainsKey(objectID) && objectTimersUI.ContainsKey(objectID))
                {
                    GameObject timerUI = objectTimersUI[objectID];
                    if (timerUI != null)
                    {
                        timerUI.transform.position = Camera.main.WorldToScreenPoint(interactableObjects[objectID].transform.position) + new Vector3(4.0f, 5.0f, 0);
                    }
                }
            }
        }

        foreach (string objectID in completedObjects)
        {
            if (objectTimersUI.ContainsKey(objectID))
            {
                GameObject timerUI = objectTimersUI[objectID];
                if (timerUI != null && !timerUI.CompareTag("GameTimer"))
                {
                    Destroy(timerUI); 
                }
                objectTimersUI.Remove(objectID);
            }
        }

        respawnTimer += Time.deltaTime;
        if (respawnTimer >= respawnInterval)
        {
            RespawnTimers();
            respawnTimer = 0f; 
        }

        completedObjects.Clear();
    }

    private void RespawnTimers()
    {
        foreach (var objectID in interactableObjects.Keys)
        {
            InteractionObjectModel interactionObject = interactableObjects[objectID].GetComponent<InteractionObjectModel>();

            if (interactionObject != null && !interactionObject.GetIsDamaged())
            {
                if (objectTimersUI.ContainsKey(objectID))
                {
                    GameObject timerUI = objectTimersUI[objectID];
                    if (timerUI != null)
                    {
                        Destroy(timerUI); 
                    }
                    objectTimersUI.Remove(objectID);
                }
                objectTimers.Remove(objectID);
                Debug.Log($"Timer for objectID {objectID} was removed because it is no longer damaged.");
                continue;
            }

            if (!excludedObjectNames.Contains(interactableObjects[objectID].GetComponent<InteractionObjectModel>().ObjectName) && !interactableObjects[objectID].CompareTag("Player"))
            {
                if (!objectTimers.ContainsKey(objectID) && !objectTimersUI.ContainsKey(objectID))
                {
                    if (Random.value <= creationChance)
                    {
                        objectTimers[objectID] = (Random.Range(10.0f, 12.0f), false); // Reset the timer value and mark as not completed
                        GameObject newTimerUI = Instantiate(timerPrefab, uiCanvas.transform);
                        newTimerUI.GetComponent<Timer1>().startTime = objectTimers[objectID].time;
                        objectTimersUI.Add(objectID, newTimerUI);

                        newTimerUI.transform.localPosition = new Vector3(4.0f, 5.0f, 0); 
                        interactableObjects[objectID].GetComponent<InteractionObjectModel>().SetIsDamaged(true);
                    }
                }
            }
        }
    }

    // Handles events for objects when their timer reaches zero
    void HandleEvent(string objectID)
    {
        Debug.Log($"Event triggered for objectID: {objectID}");

        GameObject obj = interactableObjects[objectID];

        // Check if the InteractionObjectModel script is present
        InteractionObjectModel interactionObject = obj.GetComponent<InteractionObjectModel>();
        if (interactionObject != null)
        {
            interactionObject.SetIsDead(true); // Mark the object as dead
            Debug.Log($"{interactionObject.ObjectName} is now dead.");
        }

        // Apply damage to health bar
        if (healthBar != null)
        {
            healthBar.healthDamage(true); // Damaging health directly from EventsDraft
            Debug.Log("Damaged health due to timer completion.");
        }

        // Reactivate and reset the timer UI if needed
        if (objectTimersUI.ContainsKey(objectID))
        {
            GameObject timerUI = objectTimersUI[objectID];
            if (timerUI != null)
            {
                timerUI.SetActive(true);
                timerUI.GetComponent<Timer1>().startTime = Random.Range(10.0f, 12.0f); // Resetting the Timer script start time
            }
        }
    }
}
