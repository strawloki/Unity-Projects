using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gun_addItem : MonoBehaviour
{
    // Start is called before the first frame update

    public AudioClip fire, reload, empty;
    void Start()
    {
        if(gameObject.GetComponent<Item>() == null && gameObject.GetComponent<Weapon>() == null)
        {
            if (gameObject.GetComponent<Rigidbody>() == null) gameObject.AddComponent<Rigidbody>();

            Item item = gameObject.AddComponent<Item>();
            Weapon weapon = gameObject.AddComponent<Weapon>();
            weapon.CreateGun((int)Weapon.weaponIDs.PEESTOL, 50, fire, reload, empty);

            item.name = weapon.GunName;
            item.tag = "gun";
            item.canBePickedUp = true;
            item.actionID = (int)Player.actionIDs.SHOOT_MODE;
            item.ItemID = (int)Item.itemid.GUN_PEESTOL;

            


        }
        else
        {
            Item item = gameObject.GetComponent<Item>();

            Weapon weapon = gameObject.GetComponent<Weapon>();

            item.name = weapon.GunName;
            item.tag = "gun";
            item.canBePickedUp = true;
            item.actionID = (int)Player.actionIDs.SHOOT_MODE;
            item.ItemID = (int)Item.itemid.GUN_PEESTOL;


        }
    }

}
