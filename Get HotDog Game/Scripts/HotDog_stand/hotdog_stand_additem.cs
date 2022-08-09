using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hotdog_stand_additem : MonoBehaviour
{
   
    void Start()
    {
        gameObject.AddComponent<Rigidbody>();

        Item item = gameObject.AddComponent(typeof(Item)) as Item;
        item.name = "Hotdog Stand";
        item.tag = "prop";
        item.canBePickedUp = false;
        item.actionID = 2; //order a hotdog
    }

    
}
