using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSource : MonoBehaviour
{
    public bool lightningStrike; 
    private FireEvent fireEvent;

    void Start()
    {
        fireEvent = GetComponent<FireEvent>();
    }

    void Update()
    {
        if (lightningStrike)
        {
            fireEvent.TriggerFire();
            lightningStrike = false; 
        }
    }
}
