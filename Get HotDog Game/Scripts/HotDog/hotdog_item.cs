using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hotdog_item : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if(gameObject.GetComponent<Rigidbody>() == null)
        {
            gameObject.AddComponent<Rigidbody>();
        }

        
        if (gameObject.GetComponent<BoxCollider>() == null)
        {
            gameObject.AddComponent<BoxCollider>();
            gameObject.GetComponent<BoxCollider>().size = new Vector3(0.26f, 0.16f, 1f);
            gameObject.GetComponent<BoxCollider>().center = new Vector3(0f, 0.54f, 0f);
        }


        if (gameObject.GetComponent<Item>() != null)
        {
            Item item = gameObject.GetComponent<Item>();

            item.name = "Hot Dog";
            item.tag = "edible";
            item.value = 2.5f;
            item.canBePickedUp = true;
            item.actionID = 1;
        }

        else
        {
            Item item = gameObject.AddComponent(typeof(Item)) as Item;

            item.name = "Hot Dog";
            item.tag = "edible";
            item.value = 2.5f;
            item.canBePickedUp = true;
            item.actionID = 1;
        }
        


            
    }

   
}
