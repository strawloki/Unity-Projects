using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;



public class poponarTrigger : MonoBehaviour
{
    PlayableDirector audioData;
    GameObject cone;


    void Start()
    {
        cone = GameObject.Find("trigger_cone");
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            Player player = other.gameObject.GetComponent<Player>();

            player.AccessToPopoDoor = true;
            cone.SetActive(false);

            audioData = GetComponent<PlayableDirector>();
            audioData.Play();
            
        }
    }
}
