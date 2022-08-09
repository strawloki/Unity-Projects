using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class StallManager : MonoBehaviour
{
    public GameObject stallPrefab;



    int timerCounter = 0;
    
    Vector3[] stallLocations = new Vector3[6] {
        new Vector3(10f,1f,20f),
        new Vector3(12f,1f,20f),
        new Vector3(14f,1f,20f),
        new Vector3(16f,1f,20f),
        new Vector3(18f,1f,20f),
        new Vector3(20f,1f,20f)
    };

    GameObject[] stallGO = new GameObject[6];
  

    // Start is called before the first frame update
    FunctionTimer fc, countdownTimer;

    int TimerInSeconds = 60;
    GameObject testTextGO;
    void Start()
    {
        //create the stalls 
        
        SetLuckyStall();
        //fc = FunctionTimer.CreateTimer(test, 2f, true);
        //countdownTimer = FunctionTimer.CreateTimer(testTwo, 1f, true);

        testTextGO  = GameObject.Find("testText");

        if(testTextGO != null)
        {
            countdownTimer = FunctionTimer.CreateTimer(DisplayTimer, 1f, true);
        }
        else Debug.Log("ERROR: Cannot find the testText gameOBject.");

    }
    void test()
    {
        timerCounter++;

        if(timerCounter >= 5) fc.Repeat = false; 
        Debug.Log("IT'S OVER! (" + timerCounter.ToString() + ")");
    }

    void testTwo()
    {
        Debug.Log(TimerInSeconds.ToString());
        //TimerInSeconds--;

        if(TimerInSeconds < 1) countdownTimer.Repeat = false; //do stuff here, like end the game 
        else TimerInSeconds--;
        
        
    }

    void DisplayTimer()
    {
        Text text = testTextGO.GetComponent<Text>();

        text.text = TimerInSeconds.ToString();

        if(TimerInSeconds < 1) countdownTimer.Repeat = false;
        else TimerInSeconds--;
    }
    void SetLuckyStall()
    {
        System.Random rand = new System.Random();

        int luckyStall = rand.Next(0, stallGO.Length);
        Debug.Log("Stall number " + luckyStall.ToString() + " is the lucky stall");
        Stall stl = new Stall();

        for(int i = 0; i < stallGO.Length; i++)
        {
            /*
            if(i == luckyStall){
                stalls[i].IsLucky = true;
            }
            else stalls[i].IsLucky = false;
            */

                if(stallPrefab != null) {
                    stallGO[i] = Instantiate(stallPrefab, stallLocations[i], Quaternion.identity);
                    stallGO[i].AddComponent<Stall>();
                    stallGO[i].GetComponent<Stall>().Number = i;
                    

                    stl = stallGO[i].GetComponent<Stall>();
                    stl.Valid = true;
                    
                    if(stl.Number == luckyStall && stl.Valid) { stl.IsLucky = true;  }
                    else if(stl.Number != luckyStall && stl.Valid) { stl.IsLucky = false; }

                    
                }

                //stl = new Stall();
                
               
            
        }
    }


    
    
    
}
