using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    string ItemName;
    string ItemTag;
    float ItemValue;
    bool CanBePickedUp = false;
    int ActionID, itemID;

    public enum itemid
    {
        INVALID,
        HOTDOG,
        GUN_PEESTOL
    }


    public string name
    {
        get { return ItemName; }
        set { ItemName = value; }
    }

    public string tag
    {
        get { return ItemTag; }
        set { ItemTag = value; }
    }
    public int ItemID
    {
        get { return itemID; }
        set { itemID = value; }
    }
    public float value
    {
        get { return ItemValue; }
        set { ItemValue = value; }

    }
    public bool canBePickedUp
    {
        get { return CanBePickedUp; }
        set { CanBePickedUp = value; }
    }
    public int actionID
    {
        get { return ActionID; }
        set { ActionID = value; }
    }
    public static Item ReturnEmpty()
    {
        Item result = new Item();
        result.name = "empty";
        result.canBePickedUp = false;
        return result;
    }
    public static Item ReturnItem(int p_itemID)
    {
        

        GameObject result = new GameObject();
        result.AddComponent<Item>();

        switch(p_itemID)
        {
            case (int) itemid.HOTDOG:
                
                result.GetComponent<Item>().name = "Hot Dog";
                result.GetComponent<Item>().tag = "edible";
                result.GetComponent<Item>().value = 2.5f;
                result.GetComponent<Item>().canBePickedUp = true;
                result.GetComponent<Item>().actionID = 1;
                result.GetComponent<Item>().itemID = (int)itemid.HOTDOG;

                
                break;

            case (int) itemid.GUN_PEESTOL:

                result.GetComponent<Item>().name = "Peestol";
                result.GetComponent<Item>().tag = "gun";
                result.GetComponent<Item>().canBePickedUp = true;
                result.GetComponent<Item>().actionID = 1;
                result.GetComponent<Item>().itemID = (int)itemid.GUN_PEESTOL;

                break;
            default:
                result = EmptyGO();
                Debug.Log("wrong id somewhere at stand");
                break;

        }

        return result.GetComponent<Item>();
    }

    public void doAction(int actionID)
    {
       
        switch(actionID)
        {
            case 0:
            case 1:
                break;

            case 2:

                Vector3 hotdog_spawn = GameObject.Find("HotDog_Spawn").transform.position;

                GameObject hotdog_prefab = GameObject.Find("hotdog");


                Instantiate(hotdog_prefab, hotdog_spawn, Quaternion.Euler(0f, 0f, 0f));

                break;

        }
    }
    public static GameObject EmptyGO()
    {
        GameObject result = new GameObject();
        
        result.AddComponent<Item>();
        result.AddComponent<Rigidbody>();

        Item item = result.GetComponent(typeof(Item)) as Item;

        item.name = "empty";
        item.CanBePickedUp = false;

        return result;

    }
    public void CreateItem(int id, string name)
    {
        switch(id)
        {
            case (int)itemid.HOTDOG:
                break;

            case (int)itemid.GUN_PEESTOL:
                this.name = name;
                this.tag = "gun";
                this.canBePickedUp = true;
                this.actionID = (int)Player.actionIDs.SHOOT_MODE;

                break;

            default: Debug.Log("Item.cs @ CreateItem() : ID entered found no match."); break;
        }
    }
}
