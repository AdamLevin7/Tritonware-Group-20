using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventsDraft : MonoBehaviour
{
    public Dictionary<string, float> objectTimers = new Dictionary<string, float>();
    public Dictionary<string, GameObject> interactableObjects = new Dictionary<string, GameObject>();
    public Dictionary<string, GameObject> objectTimersUI = new Dictionary<string, GameObject>();

    public GameObject timerPrefab;  // Prefab reference for Timer
    public Canvas uiCanvas;         // Reference to the UI Canvas
    private ModifyHealthBar healthBar; // Reference to ModifyHealthBar

    // List of object names to exclude from having timers
    public List<string> excludedObjectNames = new List<string>(); // Add names in the Inspector

    void Start()
    {
        // Initialize the interactableObjects dictionary
        foreach (var interactionObject in FindObjectsOfType<InteractionObjectModel>())
        {
            string objectID = interactionObject.objectID;
            interactableObjects.Add(objectID, interactionObject.gameObject);

            // Check if the objectName is in the excluded list
            if (!excludedObjectNames.Contains(interactionObject.ObjectName))
            {
                // Initialize timers for each object with a random time
                objectTimers[objectID] = Random.Range(10.0f, 12.0f); // Set a random initial time

                // Instantiate Timer prefab and link to object
                GameObject timerUI = Instantiate(timerPrefab, uiCanvas.transform); // Instantiate under the UI Canvas
                timerUI.GetComponent<Timer1>().startTime = objectTimers[objectID];
                objectTimersUI.Add(objectID, timerUI);

                timerUI.transform.localScale = new Vector3(2.0f, 2.0f, 1); // Scale up if necessary

                // Set the initial position of the timer UI
                timerUI.transform.localPosition = new Vector3(4.0f, 5.0f, 0); // Adjust based on your UI layout
            }
            else
            {
                // Log excluded objects if needed
                Debug.Log($"Excluding timer for object: {interactionObject.ObjectName}");
            }
        }

        // Find the health bar in the scene
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
            objectTimers[objectID] -= Time.deltaTime;

            // When a timer reaches zero, trigger the associated event
            if (objectTimers[objectID] < 0)
            {
                HandleEvent(objectID);
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

        // Reset timers for completed objects
        foreach (string objectID in completedObjects)
        {
            if (objectTimersUI.ContainsKey(objectID))
            {
                GameObject timerUI = objectTimersUI[objectID];
                if (timerUI != null)
                {
                    Destroy(timerUI); // Destroy the UI object
                }
                objectTimersUI.Remove(objectID);
            }

            // Create a new timer instance for the object if it is not excluded
            if (!excludedObjectNames.Contains(interactableObjects[objectID].GetComponent<InteractionObjectModel>().ObjectName))
            {
                objectTimers[objectID] = Random.Range(10.0f, 12.0f); // Reset the timer value
                GameObject newTimerUI = Instantiate(timerPrefab, uiCanvas.transform); // Instantiate under the UI Canvas
                newTimerUI.GetComponent<Timer1>().startTime = objectTimers[objectID];
                objectTimersUI.Add(objectID, newTimerUI); // Add the new timer to the dictionary

                newTimerUI.transform.localPosition = new Vector3(4.0f, 5.0f, 0); // Adjust based on your UI layout
            }
        }

        completedObjects.Clear();
    }

    // Handles events for objects when their timer reaches zero
    void HandleEvent(string objectID)
    {
        Debug.Log($"Event triggered for objectID: {objectID}");

        GameObject obj = interactableObjects[objectID];
        if (healthBar != null)
        {
            healthBar.healthDamage(true); // Damaging health directly from EventsDraft
            Debug.Log("Damaged health due to timer completion.");
        }

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
