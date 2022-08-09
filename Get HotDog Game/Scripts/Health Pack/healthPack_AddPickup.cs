using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healthPack_AddPickup : MonoBehaviour
{
    public AudioClip sound;

    void Start()
    {
        Pickup pickup = gameObject.AddComponent<Pickup>();
        pickup.CreatePickup((int)Pickup.pickupIDs.HEALTH_FULL, false);
        pickup.SpinEffect(this.gameObject);

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {

            Pickup.OnPickUpEnter(GetComponent<Pickup>().ID, this.gameObject, sound);

            //audioSource.Play();
        }
    }

}
