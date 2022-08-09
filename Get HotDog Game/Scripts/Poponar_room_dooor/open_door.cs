using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class open_door : MonoBehaviour
{
    /*
    Quaternion closedRotation = Quaternion.Euler(0f,180f,0f);
    Quaternion openRotation = Quaternion.Euler(110f, 0f, 0f);
    
    */
    static GameObject go_pivot; //could make this public for each door
    SphereCollider handleCollider;
    
    public float doorSpeed = 15f;

    static float timeAtOpening = 0.0f;
    static float timeAtClosing = 0.0f;
    static float OpeningTime = 0.875f;

    bool inCollider;
    static bool openDoor = false;

    void Start()
    {
        handleCollider = transform.GetChild(0).GetComponent<SphereCollider>();
    }

    public static void OpenDoor(GameObject Pivot)
    {
        
        timeAtOpening = Time.time;
        openDoor = true;
        go_pivot = Pivot;
    }

    
    public static void CloseDoor()
    {
        openDoor = false;
        
    }

    public void Update()
    {

        if (openDoor && go_pivot != null)
        {
            gameObject.GetComponent<BoxCollider>().enabled = false;

            float rotation = 120 * Time.deltaTime;

            go_pivot.transform.rotation *= Quaternion.AngleAxis(rotation, new Vector3(0, 0, 1));
            handleCollider.GetComponent<SphereCollider>().enabled = false;

            if (Time.time > timeAtOpening + OpeningTime)
            {


                openDoor = false;
                timeAtClosing = Time.time;

            }

        }
        else
        {


            if (go_pivot != null && !openDoor)
            {
                if (Time.time > timeAtClosing + (OpeningTime + 5.0f))
                {

                    float rotation = -120 * Time.deltaTime;

                    gameObject.GetComponent<BoxCollider>().enabled = true;
                    if (Time.time < (timeAtClosing + (OpeningTime + 5.0f)) + OpeningTime) go_pivot.transform.rotation *= Quaternion.AngleAxis(rotation, new Vector3(0, 0, 1)); handleCollider.GetComponent<SphereCollider>().enabled = true;

                }

            }
        }

    }
}
