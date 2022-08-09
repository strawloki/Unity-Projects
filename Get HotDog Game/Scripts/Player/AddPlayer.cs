using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Player player = gameObject.AddComponent<Player>() as Player;
        player.name = "Main Player";
        player.cash = 19.3f;
        player.hunger = 1.0f;
        player.InitializeInventory();
    }

}
