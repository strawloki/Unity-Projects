using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public GameObject Pivot;
    int DoorID;

    public int doorID
    {
        get { return DoorID; }

        set { DoorID = value; }
    }

    public void OpenDoor()
    {
        if(Pivot != null)
        {
            float rotation = 90 * Time.deltaTime;
        }
    }

}
