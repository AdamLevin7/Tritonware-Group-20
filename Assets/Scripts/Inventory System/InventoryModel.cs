using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryModel : MonoBehaviour
{
    private static InventoryModel instance;
    public InventoryView inventoryView;

    // Item quantities associated with item names
    public Dictionary<string, int> itemQuantities = new Dictionary<string, int>();

    // Called when the script instance is being loaded
    void Awake()
    {
        // Ensure there is only one instance of InventorySystem and it persists between scenes
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject); // Keeps this object alive across scenes
        }
        else
        {
            Destroy(gameObject); // Destroys duplicate instances of the InventoryManager
        }

        // Initializes inventory items with default amounts
        itemQuantities["water"] = 0;
        itemQuantities["apple"] = 0;
        itemQuantities["wheat"] = 0;
        itemQuantities["wood"] = 0;
        itemQuantities["barbShot"] = 0;
               
        if (inventoryView == null)
        {
            inventoryView = FindObjectOfType<InventoryView>();
        }
    }

    // Define maximum quantities for certain item names
    public Dictionary<string, int> maxItemQuantities = new Dictionary<string, int>
    {
        { "water", 1000 },
        { "apple", 10 },
        { "wheat", 1 },
        { "wood", 1 },
        { "barbShot", 1 }
    };

    // For Collectables
    public Dictionary<string, string> objectToItemNames = new Dictionary<string, string>
    {
        { "Pond", "water" },
        { "AppleTree", "apple" },
        { "Wheat", "wheat" },
        { "ChoppableTree", "wood" },
        { "", "barbShot" }
    };

    private void SetItemQuantity(string itemName, int newQuantity)
    {
        // Update the item quantity in the dictionary
        itemQuantities[itemName] = newQuantity;
        Debug.Log(newQuantity);

        // Call the InventoryView's UpdateInventoryUI to refresh the UI
        if (inventoryView != null)
        {
            inventoryView.UpdateItemUI(newQuantity, itemName);
        }
        else
        {
            Debug.LogWarning("InventoryView reference is missing.");
        }
    }

    // Just for testing InteractionController right now (dummy script)
    public bool CheckHealItemByName(string objectName)
    {
        // Water check for burning
        if (new List<string> { "Barn", "ChoppableTree", "AppleTree", "Wheat" }.Contains(objectName))
        {
            if (itemQuantities["water"] >= 1)
            {
                SetItemQuantity("water", itemQuantities["water"] - 1);
            }
            else
            {
                return false;
            }
            return true;
        }
        // Food check for hunger
        if (new List<string> { "Horse", "Chicken", "Cow" }.Contains(objectName))
        {
            if (itemQuantities["wheat"] >= 1)
            {
                SetItemQuantity("wheat", itemQuantities["wheat"] - 1);
            }
            else if (itemQuantities["apple"] >= 1)
            {
                SetItemQuantity("apple", itemQuantities["apple"] - 1);
            }
            else
            {
                return false;
            }
            return true;
        }
        return false; // Add default return to avoid missing return path
    }

    public bool CheckReviveItemByName(string objectName)
    {
        // Wood check for repairing wooden object
        if (new List<string> { "Barn", "ChoppableTree", "AppleTree", "FarmPen" }.Contains(objectName))
        {
            if (itemQuantities["wood"] >= 1)
            {
                SetItemQuantity("wood", itemQuantities["wood"] - 1);
            }
            else
            {
                return false;
            }
            return true;
        }
        // Free repair of wheat
        else if (objectName == "Wheat")
        {
            return true;
        }
        return false; // Add default return to avoid missing return path
    }

    public void AddItemByName(string objectName, int amount)
    {
        if (objectName == "Pond")
        {
            int currentQuantity = itemQuantities["water"];
            int maxQuantity = maxItemQuantities["water"];

            // Add the amount to the current quantity
            currentQuantity += amount;

            // Clamp the value between 0 and the max quantity
            currentQuantity = Mathf.Clamp(currentQuantity, 0, maxQuantity);
            SetItemQuantity("water", currentQuantity);
        }
        else if (objectName == "AppleTree")
        {
            int currentQuantity = itemQuantities["apple"];
            int maxQuantity = maxItemQuantities["apple"];

            // Add the amount to the current quantity
            currentQuantity += amount;

            // Clamp the value between 0 and the max quantity
            currentQuantity = Mathf.Clamp(currentQuantity, 0, maxQuantity);
            SetItemQuantity("apple", currentQuantity);
        }
        else if (objectName == "ChoppableTree")
        {
            int currentQuantity = itemQuantities["wood"];
            int maxQuantity = maxItemQuantities["wood"];

            // Add the amount to the current quantity
            currentQuantity += amount;

            // Clamp the value between 0 and the max quantity
            currentQuantity = Mathf.Clamp(currentQuantity, 0, maxQuantity);
            SetItemQuantity("wood", currentQuantity);
        }
        else if (objectName == "Wheat")
        {
            int currentQuantity = itemQuantities["wheat"];
            int maxQuantity = maxItemQuantities["wheat"];

            // Add the amount to the current quantity
            currentQuantity += amount;

            // Clamp the value between 0 and the max quantity
            currentQuantity = Mathf.Clamp(currentQuantity, 0, maxQuantity);
            SetItemQuantity("wheat", currentQuantity);
        }
        else
        {
            Debug.Log("No Item Added");
        }
    }
    //For testing
    public void Update()
    {
        if (Input.GetKeyUp(KeyCode.E))
        {
            AddItemByName("Pond", 10);
        }
    }
}
