using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    string playerName;
    float playerCash, playerHealth = 100f;
    float playerHunger = 1.0f;
    bool alive = true;
    bool accessToPopoDoor;
    GameObject objectInHand;




    void Awake()
    {
        objectInHand = new GameObject(); 
    }

    bool gunMode = false;

    public enum actionIDs
    {
        INVALID,
        EAT,
        SHOOT_MODE
    }


    public GameObject[] playerInventory = new GameObject[10];

    //bool IsPlayerHungry = false;

    //fatigue, thirst etc to be added later

    public string name
    {
        get { return playerName; }
        set { playerName = value; }
    }

    public float cash
    {
        get { return playerCash; }
        set { playerCash = value; }
    }
    public float health
    {
        get { return playerHealth; }
        set { playerHealth = value; }
    }

    public float hunger
    {
        get { return playerHunger; }
        set { playerHunger = value; }
    }

    public bool AccessToPopoDoor
    {
        get { return accessToPopoDoor; }
        set { accessToPopoDoor = value; }
    }
    public bool OnGunMode
    {
        get { return gunMode; }
        set { gunMode = value; }
    }

    public bool IsAlive
    {
        get { return alive; }
        set { alive = value; }
    }

    public GameObject ObjectInHand
    {
        get { return objectInHand; }
        set { objectInHand = value; }
    }

    public void GivePlayerMoney(float amount)
    {
        playerCash += amount;
    }
    public bool IsPlayerHungry()
    {
        bool result;
        if (playerHunger < 0.15) result = true;
        else result = false;

        return result;
    }
    public void onDeath()
    {
        this.alive = false;
        if(gameObject.GetComponent<item_mouse_interaction>() != null) gameObject.GetComponent<item_mouse_interaction>().enabled = false;
        if(gameObject.GetComponent<MouseMovement>() != null) gameObject.GetComponent<MouseMovement>().enabled = false;
        if(gameObject.GetComponent<Movement>() != null) FreezePlayer();

        Debug.Log("Player" + this.playerName + " has died");

        AudioSource deathSound = GetComponent<AudioSource>();
        if(deathSound != null) deathSound.Play(0);

        if (deathSound != null) StartDeathSequence();
    }

    void StartDeathSequence()
    {
        GameObject deathCam = GameObject.Find("deathScreenStart");

        Camera cam = deathCam.GetComponent<Camera>();
        Camera m_cam = Camera.main;
        if(m_cam.enabled)
        {
            cam.enabled = true;
            m_cam.enabled = false;
        }

        float timeElapsed = 0f, duration = 10f;

        StartCoroutine(DeathCam(deathCam, timeElapsed, duration));
        
    }

    IEnumerator DeathCam(GameObject d_cam, float timeElapsed, float duration)
    {
        StartCoroutine(d_camPullUp(d_cam, timeElapsed, duration));
        ButtonManager.EnableDeathMenu();

        while (true) //TODO: change to be time dependent 
        {

            //yield return StartCoroutine(d_camPullUp(d_cam, timeElapsed, duration));

            yield return StartCoroutine(d_camSwingRight(d_cam, 0f));

            yield return StartCoroutine(d_camSwingLeft(d_cam, 0f));
        }


    }

    IEnumerator d_camPullUp(GameObject d_cam, float timeElapsed, float duration)
    {
        
        Vector3 targetCamPos = d_cam.transform.position + new Vector3(0f,10f,0f);

        //float speed = 0.09f;
        while(timeElapsed < duration)
        {
            timeElapsed += Time.deltaTime;
            yield return d_cam.transform.position = Vector3.Lerp(d_cam.transform.position, targetCamPos, timeElapsed / duration);

            yield return null;
        }
        
    }

    IEnumerator d_camSwingRight(GameObject d_cam, float timeElapsed)
    {

        
        Quaternion targetRot = Quaternion.Euler(133f,-81f,-95f);

        float swingTime = 4.20f;

        while (timeElapsed < swingTime)
        {
            timeElapsed += Time.deltaTime; //GetComponent<Transform>()
           d_cam.transform.rotation = Quaternion.Slerp(d_cam.transform.rotation, targetRot, timeElapsed / swingTime * Time.deltaTime);
            yield return null;

        }
        timeElapsed = 0f;
        
    }

    IEnumerator d_camSwingLeft(GameObject d_cam, float timeElapsed)
    {
        
        Quaternion targetRot = Quaternion.Euler(135.6f,-247.6f,-236.5f);

        float swingTime = 4.20f;

        while (timeElapsed < swingTime)
        {
            timeElapsed += Time.deltaTime; //GetComponent<Transform>()
           d_cam.transform.rotation = Quaternion.Slerp(d_cam.transform.rotation, targetRot, timeElapsed / swingTime * Time.deltaTime);
            yield return null;

        }
        timeElapsed = 0f;
        
    }

    public void FreezePlayer()
    {
        gameObject.GetComponent<Movement>().enabled = false;
    }
    public void UnfreezePlayer()
    {
        gameObject.GetComponent<Movement>().enabled = true;
    }

    public Weapon ReturnWeaponInUse()
    {
        Weapon weapon = null;

        if (objectInHand.GetComponent<Weapon>() != null) weapon = objectInHand.GetComponent<Weapon>();
        

        return weapon;
    }

    public void BuyItem(Item item) //make into int, return 0 or 1 for failure or success
    {
        playerCash -= item.value;
        Debug.Log("Player " + playerName + " has bought item " + item.name + " at a cost of " + item.value + ". Player balance: " + playerCash);
        
    }
    public bool CanPlayerBuyItem(int itemID)
    {
        bool result = false;

        switch(itemID)
        {

            case 0:
                result = false;
                break;
            case 1: //hotdog id from now on

                if(playerCash < 2.5f) result = false;
                else result = true;
                
                break;
        }

        
        return result;
    }
    public void DoItemAction(int actionID, GameObject item)
    {
        
        switch(actionID)
        {
            case (int)actionIDs.INVALID: break;
            case (int)actionIDs.EAT: //eating an item 
                if (this.hunger < 1f)
                {
                    if (this.hunger + 0.075f < 1f) this.hunger += 0.075f;
                    else this.hunger = 1f;

                }
               
                break;

            case (int)actionIDs.SHOOT_MODE:
               
                gunMode = !gunMode;
                if(gunMode) item.transform.GetComponent<Weapon>().InUse = true;
                else item.transform.GetComponent<Weapon>().InUse = false;

                break;
        }
    }
    public void AddPlayerHunger(float hunger)
    {
        this.hunger -= hunger;
    }
    public void SetPlayerHunger(float hunger)
    {
        this.hunger = hunger;
    }

    public void TakeDamage(float damage)
    {

        //eventually add taking damage sounds
        if (damage > this.playerHealth)
        {
            this.playerHealth = 0;
            if (this.playerHealth < 1f) this.onDeath(); 

            //die
        }

        else this.playerHealth -= damage; 


        
    }
    public void InitializeInventory()
    {
        
        
        for(int i = 0; i <= playerInventory.Length - 1; i++)
        {
            playerInventory[i] = new GameObject();
            playerInventory[i].AddComponent<Item>().name = "empty";
            playerInventory[i].GetComponent<Item>().name = "empty";
        }
    }
                                            // Item component
    public GameObject SearchInventoryForItem(Item Component)
    {
        //returns first gameObject with Item component of requested characteristics (tag, name etc.)
        int requestedID = Component.ItemID;
        GameObject requestedItem;
        
        for(int i = 0; i <= playerInventory.Length - 1; i++)
        {
            if (playerInventory[i].GetComponent<Item>() != null && playerInventory[i].GetComponent<Item>().ItemID == requestedID)
            {
                requestedItem = playerInventory[i];

                break;

            }
        }

        Debug.Log("Could not find item " + Component.name);

        requestedItem = null;
        return requestedItem;
    }







}
