using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEnter : MonoBehaviour
{
    

    void OnTriggerEnter(Collider other)
    {
        Stall stall = transform.parent.GetComponent<Stall>();

        if(stall != null)
        {
            if(stall.IsLucky) Debug.Log("Stall number " + stall.Number + " is lucky!");
            else Debug.Log("Stall number " + stall.Number + " is not lucky.");
        }
        else Debug.Log("Could not find Stall component in parent object.");
    }

}
