using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionView : MonoBehaviour
{
    // Sprite variables for different states
    public Sprite defaultSprite;
    public Sprite damagedSprite;
    public Sprite deadSprite;

    // Specifically for crops, sprites for different growth stages
    public Sprite[] cropGrowthSprites; // Array of sprites for each growth stage

    private SpriteRenderer spriteRenderer; // Reference to the SpriteRenderer component
    private InteractionObjectModel interactionObjectModel; // Reference to the InteractionObjectModel

    void Awake()
    {
        // Get the SpriteRenderer component attached to the object
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Get the InteractionObjectModel script attached to the object
        interactionObjectModel = GetComponent<InteractionObjectModel>();
    }

    // This method will be called whenever a state change happens
    public void UpdateRender()
    {
        if (interactionObjectModel == null || spriteRenderer == null)
        {
            Debug.LogError("InteractionObjectModel or SpriteRenderer not assigned!");
            return;
        }

        // Check if the object is dead
        if (interactionObjectModel.GetIsDead())
        {
            RenderDeadSprite();
        }
        // Else, check if the object is damaged
        else if (interactionObjectModel.GetIsDamaged())
        {
            RenderDamagedSprite();
        }
        // Else, render the default sprite
        else
        {
            RenderDefaultSprite();
        }
    }

    // Render the dead sprite
    private void RenderDeadSprite()
    {
        spriteRenderer.sprite = deadSprite;
        Debug.Log($"{interactionObjectModel.GetObjectName()} sprite set to dead.");
    }

    // Render the damaged sprite
    private void RenderDamagedSprite()
    {
        spriteRenderer.sprite = damagedSprite;
        Debug.Log($"{interactionObjectModel.GetObjectName()} sprite set to damaged.");
    }

    // Render the default sprite (with special logic for crops)
    private void RenderDefaultSprite()
    {
        // If the object is a crop, check the growth stage
        if (interactionObjectModel.GetObjectType() == "Crop")
        {
            int growthStage = interactionObjectModel.GetGrowthStage();
            // Ensure growthStage is within valid bounds
            growthStage = Mathf.Clamp(growthStage, 0, cropGrowthSprites.Length - 1);

            // Render the appropriate growth stage sprite
            spriteRenderer.sprite = cropGrowthSprites[growthStage];
            Debug.Log($"{interactionObjectModel.GetObjectName()} sprite set to growth stage {growthStage}.");
        }
        else
        {
            // Render the default sprite for non-crops
            spriteRenderer.sprite = defaultSprite;
            Debug.Log($"{interactionObjectModel.GetObjectName()} sprite set to default.");
        }
    }
}
