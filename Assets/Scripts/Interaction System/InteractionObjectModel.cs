using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionObjectModel : MonoBehaviour
{
    // 2D Collider Aspect
    public Collider2D collider2D;
    private InteractionView interactionView;
    // General Properties
    public bool IsRevivable;
    public bool IsCollectable; 
    public bool IsBurnable;
    public string ObjectType; // ie Animal, Crop, Other

    public string ObjectName; // ie Cow, Wheat, etc.

    // General States
    public bool IsDead; // True if dead
    public bool IsDamaged; // True if damaged

    // Crop-specific variables
    public string CropType; // e.g., Apple trees, Wheat
    public int GrowthStage; // 0 = seedling, 1 = growing, 2 = fully grown
    public float GrowthRate; // Speed of growth (varies per crop type)

    // Animal-specific variables
    public string AnimalType; // e.g., Cow, Chicken, Horse
    public bool IsRunningAround; // True if the animal is not contained

    // Human-readable ID creation
    public string objectID;
    private static int idCounter = 0;

    public ModifyHealthBar healthBarModifier; 

    void Awake()
    {
        if (string.IsNullOrEmpty(objectID))
        {
            GenerateUniqueID();
        }
    }

    // Generates a unique ID using the object's name and a counter
    private void GenerateUniqueID()
    {
        objectID = $"{ObjectName}_{idCounter}";
        idCounter++;
    }


    // Setter Methods General States
    public void SetIsDead(bool state)
    {
        healthBarModifier.healthDamage(state);
        IsDead = state;
        if (interactionView != null)
        {
            interactionView.UpdateRender();
        }
        if (state)
        {
            // Additional logic for when the object is dead (ie disable interaction)
            Debug.Log($"{ObjectName} is now dead.");
        }
    }

    public void SetIsDamaged(bool state)
    {
        IsDamaged = state;
        if (interactionView != null)
        {
            interactionView.UpdateRender();
        }
        if (state)
        {
            // Additional logic for when the object is damaged
            Debug.Log($"{ObjectName} is damaged and needs attention.");
            
        }
    }

    // Setter Methods for Object-Specific States
    public void SetGrowthStage(int stage)
    {
        if (ObjectType == "Crop")
        {
            GrowthStage = Mathf.Clamp(stage, 0, 2); // Clamp between 0 and 2
            Debug.Log($"{ObjectName} growth stage is now {GrowthStage}.");
            if (interactionView != null)
            {
                interactionView.UpdateRender();
            }
        }
    }

    public void SetIsRunningAround(bool state)
    {
        if (ObjectType == "Animal")
        {
            IsRunningAround = state;
            Debug.Log($"{ObjectName} is {(IsRunningAround ? "running around" : "contained")}.");
        }
    }

    // Getter Methods
    public bool GetIsRevivable() => IsRevivable;
    public bool GetIsCollectable() => IsCollectable;
    public string GetObjectType() => ObjectType;
    public string GetObjectName() => ObjectName;
    public bool GetIsDead() => IsDead;
    public bool GetIsDamaged() => IsDamaged;
    public int GetGrowthStage() => GrowthStage;
    public float GetGrowthRate() => GrowthRate;
    public bool GetIsRunningAround() => IsRunningAround;
    void Start()
    {
        collider2D = GetComponent<Collider2D>(); // Initialize 2D Collider
        interactionView = GetComponent<InteractionView>();
        if (interactionView != null)
        {
            interactionView.UpdateRender();
        }
    }
}

