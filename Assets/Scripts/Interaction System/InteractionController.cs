using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionController : MonoBehaviour
{
    public InventoryModel inventoryModel; // Reference to the inventory model

    private InteractionObjectModel interactableObject = null;

    void Start()
    {
        inventoryModel = GameObject.Find("InventoryHolder").GetComponent<InventoryModel>();

        if (inventoryModel == null)
        {
            Debug.LogError("InventoryModel not found! Make sure it is assigned.");
        }
    }   


    // This method is called when the player enters the trigger zone
    void OnCollisionEnter2D(Collision2D collider)
    {
        interactableObject = collider.gameObject.GetComponent<InteractionObjectModel>();
    }

    // This method is called when the player leaves the trigger zone
    void OnCollisionExit2D(Collision2D collider)
    {
        // When the player leaves the interaction zone, clear the interactable object reference
        if (collider.gameObject.GetComponent<InteractionObjectModel>() != null)
        {
            interactableObject = null;
        }
    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        interactableObject = collider.gameObject.GetComponent<InteractionObjectModel>();
    }

    // This method is called when the player leaves the trigger zone
    void OnTriggerExit2D(Collider2D collider)
    {
        // When the player leaves the interaction zone, clear the interactable object reference
        if (collider.gameObject.GetComponent<InteractionObjectModel>() != null)
        {
            interactableObject = null;
        }
    }

    // The Update method runs every frame
    void Update()
    {
        // Only check for key press if there is an interactable object
        if (interactableObject != null && Input.GetKey(KeyCode.E))
        {
            HandleInteraction(interactableObject);
        }
    }
    // Interaction logic based on object state
    void HandleInteraction(InteractionObjectModel interactableObject)
    {
        Debug.Log("Key pressed");
        // Check if object is dead
        if (interactableObject.GetIsDead())
        {
            // Check if revivable
            if (interactableObject.GetIsRevivable())
            {
                Debug.Log("got revive");
                // Attempt to revive using inventory model
                if (inventoryModel.CheckReviveItemByName(interactableObject.GetObjectName()))
                {
                    interactableObject.SetIsDead(false);
                    Debug.Log($"{interactableObject.GetObjectName()} has been revived.");
                }
            }
        }
        // If not revivable, check if it's damaged
        else if (interactableObject.GetIsDamaged())
        {
                // Attempt to heal using inventory model
                if (inventoryModel.CheckHealItemByName(interactableObject.GetObjectName()))
                {
                    interactableObject.SetIsDamaged(false);
                    Debug.Log($"{interactableObject.GetObjectName()} has been healed.");
                }
        }
        // If object is not dead, check if it is collectable
        else if (interactableObject.GetIsCollectable())
        {
            // For crops, check if fully grown
            if (interactableObject.GetObjectType() == "Crop" && interactableObject.GetGrowthStage() == 2)
            {
                // Add item to inventory and reset growth stage
                inventoryModel.AddItemByName(interactableObject.GetObjectName(), 1);
                interactableObject.SetGrowthStage(2);
                Debug.Log($"{interactableObject.GetObjectName()} has been harvested and its growth stage reset.");
            }
            // For non-crops, simply add the item to the inventory
            else if (interactableObject.GetObjectType() != "Crop")
            {
                inventoryModel.AddItemByName(interactableObject.GetObjectName(), 1);
                Debug.Log($"{interactableObject.GetObjectName()} has been collected.");
            }
        }
    }
}
