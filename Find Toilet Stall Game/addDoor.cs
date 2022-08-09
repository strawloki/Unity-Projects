using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class addDoor : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if(gameObject.GetComponent<Door>() == null)
        {
            Door door = gameObject.AddComponent<Door>();

            door.Name = "test door";


        }
        
    }

    // Update is called once per frame

   
}
