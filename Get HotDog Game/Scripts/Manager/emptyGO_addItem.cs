using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class emptyGO_addItem : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (gameObject.GetComponent<Item>() != null)
        {
            Item item = gameObject.GetComponent<Item>();

            item.name = "empty";
            item.tag = "edible";
            item.value = 0f;
            item.canBePickedUp = false;
            item.actionID = -1;
        }

        else
        {
            Item item = gameObject.AddComponent(typeof(Item)) as Item;

            item.name = "empty";
            item.tag = "edible";
            item.value = 2.5f;
            item.canBePickedUp = false;
            item.actionID = -1;
        }
    }

    
}
