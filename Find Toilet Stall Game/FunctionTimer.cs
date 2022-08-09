using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FunctionTimer
{

    Action action; //delegate with method

    float timer, initTimer;
    bool isOver;
    bool repeat;

    GameObject m_timerObject;

    public bool Repeat
    {
        get{return this.repeat;}
        set{repeat = value;}
    }

    public float Timer
    {
        get{return this.timer;}
        set{this.timer = value;}
    }

    public float InitTimer
    {
        get{return this.initTimer;}
        set{this.initTimer = value;}
    }



    public class MonoBehaviourHook : MonoBehaviour
    {
        public Action onUpdate;

        void Update()
        {
            if(onUpdate != null ) onUpdate();
        }

        
    }
    static GameObject gObj;
    public static FunctionTimer CreateTimer(Action action, float timer, bool repeat){ //called from gameObject

        gObj = new GameObject("FunctionTimer", typeof(MonoBehaviourHook));
        FunctionTimer funcTimer = new FunctionTimer(action, timer, gObj, repeat);

        
        if(repeat){
           //MonoBehaviourHook mh = new MonoBehaviourHook();
            //mh.mh_StartCO(funcTimer, action);

            //MonoBehaviour.StartCoroutine(co_timerRepeat(funcTimer, action));

            //StallManager.StartRepeatTimer(funcTimer, action);



            //var tempGO = new GameObject();

            GameObject manGO = GameObject.Find("StallManager");
            FunctionInvoker fi = manGO.AddComponent<FunctionInvoker>();
            //refference Manager GameObject
            //add FunctionInvoker component/class 
            //store IEnumerators and functions in the FunctionInvoker class
            //call functions thru FunctionInvoker refference variable

            //StallManager sm = tempGO.AddComponent<StallManager>();
            fi.startCO(funcTimer, action, gObj);

           
        }
        
        else gObj.GetComponent<MonoBehaviourHook>().onUpdate = funcTimer.Update;
        
        //gObj.GetComponent<MonoBehaviourHook>().onUpdate = funcTimer.Update;
        

        return funcTimer;

    }
    private FunctionTimer(Action action, float timer, GameObject timerObject, bool repeat)
    {
        this.action = action;
        this.timer = timer < 0 ? 0f : timer;
        this.initTimer = this.timer;
        this.isOver = false;
        this.repeat = repeat;
        m_timerObject = timerObject;
    }

    //call the update function from another script that derives from MonoBehaviour

    void Update()
    {
        if(!isOver) {
            this.timer -= Time.deltaTime;

            if(this.timer < 0) {
                action();
                this.isOver = true;
                UnityEngine.Object.Destroy(m_timerObject);
                

            }

        }
    }

}