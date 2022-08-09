using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class createPickup_moneyStack : MonoBehaviour
{
    public AudioClip sound;
    AudioSource audioSource;
    void Start()
    {
        Pickup pickup = gameObject.AddComponent<Pickup>();
        pickup.CreatePickup((int)Pickup.pickupIDs.LEI100, false);
        
        pickup.SpinEffect(this.gameObject);
        
        audioSource = GetComponent<AudioSource>();

    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            
            Pickup.OnPickUpEnter(GetComponent<Pickup>().ID, this.gameObject, sound);

            //audioSource.Play();
        }
    }
}
