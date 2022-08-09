using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public enum pickupIDs
    {
        INVALID,
        LEI100,
        AMMO_PEESTOL,
        HEALTH_FULL
    }
    static AudioSource player_audioSou;
    static Player player;
    void Awake()
    {
        //player_audioSou = GameObject.Find("Player").GetComponent<AudioSource>();
        
    }
    
    string p_name;
    int pickupID;

    bool canRespawn;

    public string Name 
    {
        get {return p_name;} 
        set {p_name = value;}

    }

    public int ID {
        get {return pickupID;}
        set {pickupID = value;}
    }

    public bool CanRespawn
    {
        get{return canRespawn;}
        set{canRespawn = value;}
    }



    public void SpinEffect(GameObject pickup)
    {
        StartCoroutine(co_SpinEffect(pickup));
    }

    IEnumerator co_SpinEffect(GameObject pickup)
    {
        float y = 0f;
        while(true)
        {
            

            y += Time.deltaTime * 60;

            pickup.transform.localRotation = Quaternion.Euler(-90,y,180);
            yield return null;
        }
    }

    public void CreatePickup(int id, bool respawn)
    {
        switch(id)
        {
            case (int)pickupIDs.INVALID:
                Debug.Log("CREATE_PICKUP: Invalid pickup ID.");
                break;
            case (int)pickupIDs.LEI100:
                this.pickupID = (int)pickupIDs.LEI100;
                this.p_name = "100 Lei";
                this.canRespawn = respawn;
                break;
            case (int)pickupIDs.AMMO_PEESTOL:
                this.pickupID = (int)pickupIDs.AMMO_PEESTOL;
                this.p_name = "Deagle clip (x17 bullets)";
                this.canRespawn = respawn;
                break;

            case (int)pickupIDs.HEALTH_FULL:
                this.pickupID = (int)pickupIDs.HEALTH_FULL;
                this.p_name = "Health Pack";
                this.canRespawn = respawn;
                break;
        }
    }
    
                                    //AudioClip clip
    public static void OnPickUpEnter(int pickupID, GameObject pickup_go, AudioClip clip)
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        Debug.Log("Player found onPickUpEnter name: " + player.name);

        player_audioSou = player.gameObject.GetComponent<AudioSource>();

        switch (pickupID)
        {
            case (int)pickupIDs.INVALID:
                Debug.Log("Invalid pickUp ID (0).");
                break;
            case (int)pickupIDs.LEI100:

                pickup_go.SetActive(false);
                player.GivePlayerMoney(100f);

                player_audioSou.PlayOneShot(clip, 1f);
                break;
            case (int)pickupIDs.AMMO_PEESTOL:

                Weapon gunComponent = player.ObjectInHand.GetComponent<Weapon>();



                if (player.ObjectInHand != null && gunComponent != null)
                {
                    Debug.Log("object in hand not null and has weapon component " + player.ObjectInHand.name);
                    if (gunComponent.WeaponID == (int)Weapon.weaponIDs.PEESTOL)
                    {
                        gunComponent.TotalAmmo += gunComponent.ClipSize;
                        player_audioSou.PlayOneShot(clip, 1f);

                        pickup_go.SetActive(false);
                    }
                    
                }
                else 
                {
                    GameObject gunItem = player.SearchInventoryForItem(Item.ReturnItem((int)Item.itemid.GUN_PEESTOL));

                    if (gunItem != null) { gunItem.GetComponent<Weapon>().GiveAmmo((int)Weapon.clipSize.PEESTOL); player_audioSou.PlayOneShot(clip, 1f); }
                    
                }

                

                break;
            case (int)pickupIDs.HEALTH_FULL:

                player.health = 100f;
                Debug.Log( player.name + " health is now " + player.health.ToString());

                pickup_go.SetActive(false);
                player_audioSou.PlayOneShot(clip, 1f);


                break;
        }
    }
}
