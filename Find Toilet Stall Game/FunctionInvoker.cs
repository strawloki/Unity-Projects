using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class FunctionInvoker : MonoBehaviour
{
    public void startCO(FunctionTimer timer, Action action, GameObject toDestroy)
    {
        StartCoroutine(co_timerRepeat(timer, action, toDestroy));
    }

    IEnumerator co_timerRepeat(FunctionTimer timer, Action action, GameObject toDestroy)
    {
            while(timer.Repeat)
            {
            
                timer.Timer -= Time.deltaTime;

                if(timer.Timer < 0) {
                    action();
                   
                    timer.Timer = timer.InitTimer;
                    //UnityEngine.Object.Destroy(m_timerObject);        

                }
                yield return null;

            }
            GameObject.Destroy(toDestroy);
    } 

}
