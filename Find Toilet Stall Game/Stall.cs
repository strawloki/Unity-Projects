using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Stall : MonoBehaviour
{
    bool lucky = false;
    int stallNumber;

    bool valid = false;

    public bool IsLucky
    {
        get{return lucky;}
        set{ lucky = value; }
    }

    public int Number
    {
        get{return stallNumber;}
        set{stallNumber = value;}
    }

    public bool Valid
    {
        get{return this.valid;}
        set{this.valid = value;}
    }

   public Stall()
   {
       lucky = false;
       stallNumber = -1;
       valid = false;
   } 


}
