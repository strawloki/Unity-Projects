using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chair_item : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.AddComponent<Rigidbody>();
        Item item = gameObject.AddComponent(typeof(Item)) as Item;
        item.name = "Wooden Gaming Chair";
        item.tag = "prop";
        item.value = 2.5f;
        item.canBePickedUp = true;
        item.actionID = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
