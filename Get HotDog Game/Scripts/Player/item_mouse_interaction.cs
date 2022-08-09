using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class item_mouse_interaction : MonoBehaviour
{
    bool isHolding, canPickUp;
    public float maxDistance = 5f;
    public float throwingForce = 1000f;
    int actionID;
    int inventorySlot = 0; //0 thru 9

    GameObject availableObject;
    GameObject objectInHand;
    GameObject guide;
    GameObject player;
    GameObject go_itemText;
    GameObject itemToSpawn;
    AudioSource p_ads;

    Player mc;
    bool gunMode;
    GameObject gunGuide;

    Text itemText;
    Vector3 objPos;

    void Awake()
    {
        guide = GameObject.Find("object_guide");
        player = GameObject.Find("Player");
        if (player != null) p_ads = player.GetComponent<AudioSource>();

        go_itemText = GameObject.Find("itemText");
        gunGuide = GameObject.Find("gunGuide");

        // mc = player.GetComponent(typeof(Player)) as Player;
    }

    void PlaceGun(GameObject objectInHand)
    {
        Vector3 targetLoc = gunGuide.transform.position;
        objectInHand.transform.SetParent(gunGuide.transform);
        objectInHand.transform.position = targetLoc;

       
        Quaternion player_rot = Camera.main.transform.localRotation;

        objectInHand.transform.rotation = player_rot;
        
    }


    void HoldAvailableObj()
    {
        isHolding = true;
        objectInHand = availableObject;
        objectInHand.GetComponent<Rigidbody>().useGravity = false;
        objectInHand.GetComponent<Rigidbody>().detectCollisions = false;
        objectInHand.transform.SetParent(guide.transform);
        
    }

    void DropObject()
    {
        isHolding = false;
        if (objectInHand != null)
        {
            objectInHand.GetComponent<Rigidbody>().useGravity = true;
            objectInHand.GetComponent<Rigidbody>().detectCollisions = true; //was false before
            objectInHand.transform.position = objPos;
            objectInHand.transform.SetParent(null);
            objPos = Vector3.zero;
            objectInHand = null;
        }
    }

    void DestroyObject()
    {
        isHolding = false;
        
        objPos = Vector3.zero;
        
        availableObject = null;
        Destroy(objectInHand.gameObject);
        objectInHand = null;
    }

    void ThrowObject()
    {

        if (objectInHand != null)
        {
            
            objectInHand.transform.SetParent(null);
            objectInHand.GetComponent<Rigidbody>().useGravity = true;
            objectInHand.GetComponent<Rigidbody>().detectCollisions = true;
            objectInHand.GetComponent<Rigidbody>().AddForce(guide.transform.forward * throwingForce);

            objectInHand = null;
            isHolding = false;


        }

    }
    

    void PutItemInInventory()
    {
        mc = player.GetComponent(typeof(Player)) as Player;

        
        GameObject item = objectInHand is null ? new GameObject() : objectInHand;
        

        Item itemComponent = objectInHand.GetComponent<Item>();

        Item invItemComp = item.GetComponent<Item>();

        if ( itemComponent != null)
        {
            invItemComp.CreateItem(itemComponent.ItemID, itemComponent.name);

            if (itemComponent.tag == "gun" && item.GetComponent<Weapon>() != null)
            {
                
                Weapon WepComponent = item.GetComponent<Weapon>();
                Weapon ObjComponent = objectInHand.GetComponent<Weapon>();
                WepComponent.CreateGun(ObjComponent.WeaponID, ObjComponent.TotalAmmo, ObjComponent.Fire, ObjComponent.RELOAD, ObjComponent.Empty);

                
            }
            
        }

        item.GetComponent<Rigidbody>().useGravity = false;

       
        mc.playerInventory[inventorySlot] = objectInHand;

       
        isHolding = false;
        objectInHand.transform.position = Vector3.zero;
        objectInHand.transform.SetParent(null);

        objPos = Vector3.zero;
        objectInHand = null;

    }

    
    void DisplayItemInHand()
    {
        
        string _itemText = "Item in hand: ";

        itemText = go_itemText.GetComponent(typeof(Text)) as Text;

        GameObject itemInInventory = mc.playerInventory[inventorySlot];

        if (itemInInventory != null)
        {
            Item item = itemInInventory.GetComponent(typeof(Item)) as Item;
            if (item != null) _itemText = _itemText + item.name;
            else _itemText = _itemText + "null";

            itemText.text = _itemText;

            _itemText = "Item in hand: ";
        }

        
    }


    void Update()
    {
        mc = player.GetComponent(typeof(Player)) as Player;
        gunMode = mc.OnGunMode;
        if (objectInHand != null) mc.ObjectInHand = objectInHand; //troublesome?


        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        itemToSpawn = mc.playerInventory[inventorySlot].gameObject; 
        RaycastHit hit;
        DisplayItemInHand();




        if (Physics.Raycast(ray, out hit))
        {
            Item item = hit.transform.GetComponent<Item>();

            if (item != null && !isHolding)
            {

                canPickUp = item.canBePickedUp;
                availableObject = item.transform.gameObject;
                actionID = item.actionID;
            }
            else
            {
                availableObject = null;
                actionID = 0;
            }


        }

        
        if (isHolding && objectInHand != null)
        {
            
            objectInHand.GetComponent<Rigidbody>().velocity = Vector3.zero;
            objectInHand.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;

            objPos = objectInHand.transform.position;
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (availableObject != null && !gunMode)
            {

                if (Vector3.Distance(gameObject.transform.position, availableObject.transform.position) <= maxDistance) //for the hotdog stand to spawn hotdogs
                {

                            /*  
                                     Fix CanPlayerBuyItem(use id and return int not whole gameObject dumbass)
                                                                        ||
                            */


                    if (player.GetComponent<Player>().CanPlayerBuyItem(1) && availableObject.GetComponent<Item>().name == "Hotdog Stand") 
                    {
                        
                        availableObject.GetComponent<Item>().doAction(actionID);

                        Item hotdog = Item.ReturnItem(1);
                        
                        player.GetComponent<Player>().BuyItem(hotdog);

                        Destroy(hotdog.gameObject);
                        
                        
                    }


                }



                if (!isHolding && canPickUp)
                {
                    if (Vector3.Distance(gameObject.transform.position, availableObject.transform.position) <= maxDistance) HoldAvailableObj();


                }


                if (!isHolding && !canPickUp)
                {

                    if (Vector3.Distance(gameObject.transform.position, availableObject.transform.position) <= maxDistance)
                    {

                        if (availableObject.GetComponent<Item>().tag == "door_handle" && availableObject.transform.parent.name == "doorPivot")
                        {

                            if (player.GetComponent<Player>().AccessToPopoDoor)
                            {

                                GameObject pivot = availableObject.transform.parent.gameObject;

                                open_door.OpenDoor(pivot);

                            }

                        }

                    }
                }

            }       
            else if(availableObject == null && gunMode)
            {
                Weapon gun = null;

                if (objectInHand != null) gun = objectInHand.GetComponent<Weapon>();
                else mc.OnGunMode = false;

                if (gun != null)
                {
                    gun.Shoot(gun.WeaponID, p_ads);
                }
                
            }
            else if (isHolding) DropObject();



        }
        if (Input.GetMouseButtonDown(1))
        {

            if (isHolding && !gunMode) ThrowObject();

        }

        if (Input.GetKeyDown(KeyCode.F)) //do item action
        {
            

            if (isHolding)
            {
                Player mc = player.GetComponent(typeof(Player)) as Player;
                Item item = objectInHand.GetComponent<Item>();

                mc.DoItemAction(item.actionID, objectInHand);


                if (item.tag == "edible") Destroy(objectInHand); 
                else if (item.tag == "gun") PlaceGun(objectInHand); //place the gun on the right of the player screen, facing forward
                
                

            }

        }

        if(Input.GetKeyDown(KeyCode.R) && gunMode)
        {
            Weapon wep = objectInHand.GetComponent<Weapon>();

            if (wep != null) wep.Reload(p_ads);
            else Debug.Log("Weapon is null");
        }

        if (Input.GetKeyDown(KeyCode.Q)) //inventory wheel left
        {
            
            if (inventorySlot > 0) inventorySlot--;
           

        }

        if (Input.GetKeyDown(KeyCode.E)) //inventory wheel right
        {
            if (inventorySlot < 9) inventorySlot++; 
            
        }

        if(Input.GetKeyDown(KeyCode.M)) //add item to inventory
        {
            mc = player.GetComponent(typeof(Player)) as Player;
            if (isHolding)
            {
                PutItemInInventory();
            }
            else
            {
                if (mc.playerInventory[inventorySlot].name == "emptyGO") Debug.Log($"Debug: Item in slot {inventorySlot} is empty.");

                else
                {
                    
                    if (mc.playerInventory[inventorySlot].name != "New Game Object")
                    {

                        if (itemToSpawn != null)
                        {
                            GameObject itemGO = Instantiate(itemToSpawn, player.transform.position + player.transform.forward + new Vector3(0, 1f, 0), Quaternion.identity);
                            Item itemComponent = itemToSpawn.GetComponent<Item>();
                            if (itemComponent != null)
                            {

                                if (itemComponent.tag == "gun" && itemGO.GetComponent<Weapon>() != null)
                                {

                                    Weapon WepComponent = itemGO.GetComponent<Weapon>();
                                    Weapon ObjComponent = itemToSpawn.GetComponent<Weapon>();
                                    WepComponent.CreateGun(ObjComponent.WeaponID, ObjComponent.TotalAmmo, ObjComponent.Fire, ObjComponent.RELOAD, ObjComponent.Empty);

                                }
                                else if (itemComponent.tag != "gun") Debug.Log("Taking item out of inventory: itemComponent is not null, however the tag is not gun. Tag is: " + itemComponent.tag);

                            }
                            else Debug.Log("Taking item out of inventory: itemComponent is null.");

                            itemGO.GetComponent<Rigidbody>().useGravity = true;

                            Destroy(itemToSpawn);

                            itemToSpawn = GameObject.Find("emptyGO");
                        }

                        
                        GameObject empty = GameObject.Find("emptyGO");
                        if (empty.GetComponent<Item>() != null && empty.GetComponent<Item>().name == "empty") mc.playerInventory[inventorySlot] = empty; Debug.Log("set to: " + mc.playerInventory[inventorySlot].name);
                        


                    }
                    else Debug.Log("it do be New Game Object");
                }
            }
        }


    }
}
