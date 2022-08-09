using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class ShowHungerText : MonoBehaviour
{
    GameObject go_player;
    GameObject go_hungerUItext, go_moneyText, go_ammoText;

    Player player;
    Text hungerUItext;
    Text moneyUItext;
    TextMeshProUGUI ammoUItext;

    string hungerText = "Player hunger: ";
    string moneyText = "$";
    string ammoText = "-";

    void Start()
    {
        go_player = GameObject.Find("Player");
        go_hungerUItext = GameObject.Find("hungerText");
        go_moneyText = GameObject.Find("moneyText");
        go_ammoText = GameObject.Find("ammoText");
        

    }

    // Update is called once per frame
    void Update()
    {
        player = go_player.GetComponent(typeof(Player)) as Player;
        hungerUItext = go_hungerUItext.GetComponent(typeof(Text)) as Text;
        moneyUItext = go_moneyText.GetComponent<Text>();
        ammoUItext = go_ammoText.GetComponent<TextMeshProUGUI>();
       

        float percentHunger = player.hunger * 100;

        hungerText = hungerText + percentHunger.ToString() + "%";
        hungerUItext.text = hungerText;
        hungerText = "Player hunger: ";

        moneyText = moneyText + player.cash.ToString();
        moneyUItext.text = moneyText;
        moneyText = "$";

        if (player.OnGunMode)
        {

            Weapon wep = player.ReturnWeaponInUse();

            ammoText = wep.TotalAmmo.ToString() + "-" + wep.AmmoInClip.ToString();
            ammoUItext.text = ammoText;

            ammoText = "";
        }
        else { ammoText = ""; ammoUItext.text = ammoText;  }
    }
}
