using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorhandle_AddItem : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (gameObject.GetComponent<Item>() != null)
        {
            Item item = gameObject.GetComponent<Item>();

            item.name = "poponar_doorHandle";
            item.tag = "door_handle";
            item.value = 0f;
            item.canBePickedUp = false;
            item.actionID = 0;
        }

        else
        {
            Item item = gameObject.AddComponent(typeof(Item)) as Item;

            item.name = "poponar_doorHandle";
            item.tag = "door_handle";
            item.value = 0f;
            item.canBePickedUp = false;
            item.actionID = 0;
        }
    }

    
}
