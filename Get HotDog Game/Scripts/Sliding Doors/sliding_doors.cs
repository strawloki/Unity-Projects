using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sliding_doors : MonoBehaviour
{
    GameObject go_doorLeft;
    GameObject go_doorRight;

    public float rate = 15.5f;

    public Vector3 openPosition_L;
    public Vector3 openPosition_R;

    public Vector3 closedPosition_L;
    public Vector3 closedPosition_R;

    bool open = false;
    bool inCollider = false;
    AudioSource audioData;


    void Start()
    {
        go_doorLeft = GameObject.Find("sliding_door_L");
        go_doorRight = GameObject.Find("sliding_door_R");
    }

    // Update is called once per frame
    void Update()
    {
        if (inCollider)
        {
            //play cursed clip

           


            if (open)
            {
                //slide both doors
                Transform doorLeft = go_doorLeft.transform;
                Transform doorRight = go_doorRight.transform;

                doorLeft.localPosition = Vector3.Lerp(doorLeft.localPosition, openPosition_L, rate * Time.deltaTime); 
                doorRight.localPosition = Vector3.Lerp(doorRight.localPosition, openPosition_R, rate * Time.deltaTime); 


            }
            else
            {
                Transform doorLeft = go_doorLeft.transform;
                Transform doorRight = go_doorRight.transform;

                doorLeft.localPosition = Vector3.Lerp(doorLeft.localPosition, closedPosition_L, rate * Time.deltaTime);
                doorRight.localPosition = Vector3.Lerp(doorRight.localPosition, closedPosition_R, rate * Time.deltaTime);
            }
        }
        else
        {
            //close the doors
            Transform doorLeft = go_doorLeft.transform;
            Transform doorRight = go_doorRight.transform;

            doorLeft.localPosition = Vector3.Lerp(doorLeft.localPosition, closedPosition_L, rate * Time.deltaTime);
            doorRight.localPosition = Vector3.Lerp(doorRight.localPosition, closedPosition_R, rate * Time.deltaTime);

        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            audioData = GetComponent<AudioSource>();
            audioData.Play(0);
            open = true;
            inCollider = true;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            open = false;
            inCollider = false;
        }


    }
}
