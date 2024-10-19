using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class TaskView : MonoBehaviour
{
    // UI elements
    public GameObject taskTemplate;  // Prefab for the task UI
    public GameObject popupTemplate; // Prefab for the popup UI
    public Transform scrollableContent; // Parent object for the scrollable UI content
    public float popupDuration = 3f; // Time before the popup disappears

    // Preset messages dictionary based on object names
    private Dictionary<string, string> presetMessages = new Dictionary<string, string>
    {
        { "Barn", "Object A event started!" },
        { "objectB", "Object B event initiated!" },
        { "objectC", "Object C is now active!" }
    };

    // Struct to store task data
    private class TaskData
    {
        public GameObject taskUI;
        public float remainingTime;
    }

    // Store task data with objectID (string)
    private Dictionary<string, TaskData> taskUIDictionary = new Dictionary<string, TaskData>();

    // Store popup duration timers
    private List<GameObject> activePopups = new List<GameObject>();
    private List<float> popupTimers = new List<float>();
    private int i =0;
    void Update()
    {
        // Update task countdowns
        UpdateTaskTimers();

        // Update popup timers
        UpdatePopups();

        if (Input.GetKeyUp(KeyCode.E))
        {
            i++;
            AddEventUI(i.ToString(),"Barn",15f);
        }
    }

    // Add a task UI with countdown and show popup
    public void AddEventUI(string objectID, string objectName, float eventDuration)
    {
        // Check if the object name has a preset message
        if (!presetMessages.ContainsKey(objectName))
        {
            Debug.LogError("Object name not found in preset messages dictionary!");
            return;
        }

        // Instantiate task UI
        GameObject newTaskUI = Instantiate(taskTemplate, scrollableContent);
        newTaskUI.transform.Find("TaskName").GetComponent<TextMeshProUGUI>().text = presetMessages[objectName];

        // Store task data with the countdown duration
        TaskData newTaskData = new TaskData
        {
            taskUI = newTaskUI,
            remainingTime = eventDuration
        };
        taskUIDictionary[objectID] = newTaskData;

        // Show temporary popup
        ShowPopup(objectName);
    }

    // Remove a task UI and its data
    public void DeleteEventUI(string objectID)
    {
        if (taskUIDictionary.ContainsKey(objectID))
        {
            GameObject taskToRemove = taskUIDictionary[objectID].taskUI;

            // Remove task from the dictionary
            taskUIDictionary.Remove(objectID);

            // Destroy the task UI
            Destroy(taskToRemove);
        }
    }

    // Update all task countdown timers
    private void UpdateTaskTimers()
    {
        List<string> completedTasks = new List<string>();  // List of tasks to delete

        foreach (var taskEntry in taskUIDictionary)
        {
            TaskData taskData = taskEntry.Value;
            taskData.remainingTime -= Time.deltaTime;

            // Update the UI timer text
            taskData.taskUI.transform.Find("TimerTemplate").GetComponent<TextMeshProUGUI>().text = Mathf.Ceil(taskData.remainingTime).ToString() + "s";

            // Check if the timer has reached zero
            if (taskData.remainingTime <= 0)
            {
                completedTasks.Add(taskEntry.Key);  // Mark task for deletion
            }
        }

        // Delete completed tasks
        foreach (string taskID in completedTasks)
        {
            DeleteEventUI(taskID);
        }
    }

    // Show a popup with a preset message based on the object name
    private void ShowPopup(string objectName)
    {
        // Get the preset message for the given object name
        string popupMessage = presetMessages[objectName];

        // Instantiate and set the popup UI
        GameObject popupUI = Instantiate(popupTemplate, transform);
        popupUI.transform.Find("PopupText").GetComponent<TextMeshProUGUI>().text = popupMessage;
        
        activePopups.Add(popupUI);
        popupTimers.Add(popupDuration);
    }

    // Update popup timers and remove popups when time is up
    private void UpdatePopups()
    {
        for (int i = activePopups.Count - 1; i >= 0; i--)
        {
            popupTimers[i] -= Time.deltaTime;

            // If popup timer is finished, remove the popup
            if (popupTimers[i] <= 0)
            {
                Destroy(activePopups[i]);
                activePopups.RemoveAt(i);
                popupTimers.RemoveAt(i);
            }
        }
    }
}

