using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dummy_addPlayer : MonoBehaviour
{
   
    void Start()
    {
        Player player = gameObject.AddComponent<Player>() as Player;
        player.name = "Dummy Player";
        player.cash = 1f;
        player.hunger = 1.0f;
        player.InitializeInventory();
    }

    
    
}
