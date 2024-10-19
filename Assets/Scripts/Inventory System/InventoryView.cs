using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryView : MonoBehaviour
{
    // Dictionary to map item names to their corresponding UI Text components
    public Dictionary<string, TextMeshProUGUI> itemTextFields = new Dictionary<string, TextMeshProUGUI>();

     public TextMeshProUGUI waterText;
     public TextMeshProUGUI wheatText;
     public TextMeshProUGUI appleText;
     public TextMeshProUGUI woodText;
     public TextMeshProUGUI barbShotText;

    void Awake()
    {
        // Initialize the dictionary with references to UI Text components
        itemTextFields["water"] = waterText;
        itemTextFields["wheat"] = wheatText;
        itemTextFields["apple"] = appleText;
        itemTextFields["wood"] = woodText;
        itemTextFields["barbShot"] = barbShotText;
    }

    // Method to update the text for a specific item
    public void UpdateItemUI(int amount, string itemName)
    {
        if (itemTextFields.ContainsKey(itemName))
        {
            // Update the text to reflect the new item count
            itemTextFields[itemName].text = $"{itemName}: {amount}";
        }
        else
        {
            Debug.LogError($"UI Text for '{itemName}' not found!");
        }
    }
}
