using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    private static InventorySystem instance;

    // Called when the script instance is being loaded
    void Awake()
    {
        // Ensure there is only one instance of InventorySystem and it persists between scenes
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Keeps this object alive across scenes
        }
        else
        {
            Destroy(gameObject); // Destroys duplicate instances of the InventoryManager
        }
    }

    // Just for testing InteractionController right now(ie dummy script)
    public bool CheckReviveItemByName(string name)
    {
        return true;
    }
    public bool CheckHealItemByName(string name)
    {
        return true;
    }
    public bool AddItemByName(string name)
    {
        return true;
    }
}
