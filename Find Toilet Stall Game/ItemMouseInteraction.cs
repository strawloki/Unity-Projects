using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMouseInteraction : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /*
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;

        if(Physics.Raycast(ray, out hit))
        {
            Door doorComp = hit.transform.GetComponent<Door>();

            if(doorComp != null)
            {
                Debug.Log("Congratulations, you hit a door.");
            }
            
        }
        */

        if(Input.GetMouseButtonDown(0))
        {
            //Debug.Log("clicked");
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;

            if(Physics.Raycast(ray, out hit))
            {
                Door doorComp = hit.transform.GetComponent<Door>();

                if(doorComp != null)
                {
                   
                    doorComp.OpenDoor(hit.transform.gameObject);
                }
            
            }
        }
        
    }
}
