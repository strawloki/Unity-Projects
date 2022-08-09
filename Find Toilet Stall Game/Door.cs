using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    string d_name;
    bool isOpen = false;

    float swingTime = 0.5f;

    //player will hover mouse over door handle and click to open it
    //only door handle needs a 'Door' component
    //upon clicking, a static method will be called, to find the parent(actual pivoted door) and swing it open ( childObject.transform.parent.gameObject )

    public string Name
    {
        get {return d_name;}
        set {d_name = value;}
    }

    public void OpenDoor(GameObject door) //takes the door pivot object, not child
    {
        StartCoroutine(co_OpenDoor(door));

    }

    IEnumerator co_OpenDoor(GameObject door)
    {
        Vector3 openRot = new Vector3(0f, -85f, 0f);

        Quaternion q_openRot = Quaternion.Euler(openRot);
        Quaternion startRot = door.transform.rotation;

        float timeElapsed = 0f;

        while(!isOpen)
        {
            timeElapsed += Time.deltaTime;
            door.transform.rotation = Quaternion.Slerp(startRot, q_openRot, timeElapsed / swingTime);
            

            if(timeElapsed >= swingTime) isOpen = true;
            
            yield return null;

        }
        
        

        yield return new WaitForSeconds(3);

        

        
        startRot = door.transform.rotation;
        timeElapsed = 0f;

        Quaternion closedRot = Quaternion.Euler(Vector3.zero);

        while(isOpen)
        {
            
            timeElapsed += Time.deltaTime;
            door.transform.rotation = Quaternion.Slerp(startRot, closedRot, timeElapsed / swingTime);

            if(timeElapsed >= swingTime) isOpen = false;
            
            yield return null;
        }
        isOpen = false;
    }

    public static void CloseDoor()
    {

    }

}
