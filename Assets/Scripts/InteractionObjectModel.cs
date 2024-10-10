using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionObjectModel : MonoBehaviour
{
    // 2D Collider Aspect
    private Collider2D collider2D;

    // General Properties
    public bool IsRevivable { get; set; }
    public bool IsCollectable { get; set; }
    public string ObjectType { get; set; } // ie Animal, Crop, Other
    public string ObjectName { get; set; } // ie Cow, Wheat, etc.

    // General States
    public bool IsDead { get; private set; } // True if dead
    public bool IsDamaged { get; private set; } // True if damaged

    // Crop-specific variables
    public string CropType { get; set; } // e.g., Apple trees, Wheat
    public int GrowthStage { get; private set; } // 0 = seedling, 1 = growing, 2 = fully grown
    public float GrowthRate { get; set; } // Speed of growth (varies per crop type)

    // Animal-specific variables
    public string AnimalType { get; set; } // e.g., Cow, Chicken, Horse
    public bool IsRunningAround { get; private set; } // True if the animal is not contained

    // Setter Methods General States
    public void SetIsDead(bool state)
    {
        IsDead = state;
        if (state)
        {
            // Additional logic for when the object is dead (ie disable interaction)
            Debug.Log($"{ObjectName} is now dead.");
        }
    }

    public void SetIsDamaged(bool state)
    {
        IsDamaged = state;
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
    }
}

