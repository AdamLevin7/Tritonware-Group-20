using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireEvent : MonoBehaviour
{
    public InteractionObjectModel targetObject; // Reference to the object that will be affected
    public float spreadDelay = 5.0f; // Delay before the fire spreads to nearby objects
    public float spreadRadius = 5.0f; // Radius within which fire spreads

    public void TriggerFire()
    {
        Debug.Log("Fire triggered");
        if (targetObject != null)
        {
            if (!targetObject.GetIsDamaged())
            {
                targetObject.SetIsDamaged(true);
                Debug.Log($"{targetObject.GetObjectName()} is damaged by fire.");
                StartCoroutine(SpreadFireAfterDelay());
            }
            else
            {
                Debug.LogWarning($"{targetObject.GetObjectName()} is already damaged.");
            }
        }
        else
        {
            Debug.LogWarning("No target object assigned for fire event.");
        }
    }

    // Coroutine to spread fire to nearby objects after a delay
    private IEnumerator SpreadFireAfterDelay()
    {
        yield return new WaitForSeconds(spreadDelay);

        // Check nearby objects within the spread radius
        Collider2D[] nearbyObjects = Physics2D.OverlapCircleAll(transform.position, spreadRadius); // Use spreadRadius

        foreach (Collider2D collider in nearbyObjects)
        {
            InteractionObjectModel nearbyObject = collider.GetComponent<InteractionObjectModel>();
            if (nearbyObject != null && !nearbyObject.GetIsDamaged())
            {
                // Ignite the nearby object and start spreading fire from it as well
                nearbyObject.SetIsDamaged(true);
                Debug.Log($"{nearbyObject.GetObjectName()} caught fire from nearby object.");

                // Get the FireEvent component of the nearby object (if any) to continue the spread
                FireEvent nearbyFireEvent = collider.GetComponent<FireEvent>();
                if (nearbyFireEvent != null)
                {
                    StartCoroutine(nearbyFireEvent.SpreadFireAfterDelay());
                }
            }
        }
    }

    // Visualize the fire spread radius in the scene view for each object
    private void OnDrawGizmos()
    {
        // Set the color of the gizmo based on whether the object is on fire or not
        if (targetObject != null && targetObject.GetIsDamaged())
        {
            Gizmos.color = Color.red; // Red if the object is on fire
        }
        else
        {
            Gizmos.color = Color.yellow; // Yellow if it's not on fire but can spread fire
        }

        // Draw a wireframe sphere representing the fire spread radius
        Gizmos.DrawWireSphere(transform.position, spreadRadius);
    }
}
