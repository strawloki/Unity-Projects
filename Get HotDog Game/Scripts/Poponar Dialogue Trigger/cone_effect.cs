using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cone_effect : MonoBehaviour
{
    //To be attached to the trigger cone
    //UPGRADE TO SmoothDamp instead of LERP
    Vector3 downPosition = new Vector3(0f,0f,0f);
    Vector3 upPosition = new Vector3(0f, 0.135f, 0f);

    
    //public float delay = 6.2f;
    

    public float timeToTop = 1.5f;
    public float speed = 0.5f;
    float timeElapsed = 0f;
   

    bool top;

    void Start() { StartCoroutine(ConeEffect()); }
    

    IEnumerator ConeEffect()
    {
        while (true)
        {


            while (!top) yield return StartCoroutine(ConeUp());


            yield return StartCoroutine(ConeDown());
            yield return null;
        }
    }

    IEnumerator ConeUp()
    {
        while (timeElapsed < timeToTop)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, upPosition, timeElapsed / timeToTop);
            timeElapsed += Time.deltaTime * speed;
            top = false;



            yield return null;
        }

        top = true;
        timeElapsed = 0f;
    }
    IEnumerator ConeDown()
    {
        while (timeElapsed < timeToTop)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, downPosition, timeElapsed / timeToTop);
            timeElapsed += Time.deltaTime;


            yield return null;
        }

        top = false;
        
        timeElapsed = 0f;
    }
}
