using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spinCube : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject cube = GameObject.Find("coolCube");
        StartCoroutine(SpinCube(cube, 40f));
    }

    // Update is called once per frame
    

    IEnumerator SpinCube(GameObject obj, float speed)
    {
        StartCoroutine(LiftCube(obj, 50f, 1.5f));
        float y = 0;

        while(true)
        {
            y += Time.deltaTime * speed;

            obj.transform.localRotation = Quaternion.Euler(0, y, 0);
            yield return null;
        }
    }

    IEnumerator LiftCube(GameObject obj, float height, float speed)
    {
        float currentHeight = 0;
       
        Vector3 target = new Vector3(obj.transform.position.x, height, obj.transform.position.z);

        while(currentHeight <= height) //currentHeight <= height
        {
            //obj.transform.position = new Vector3(obj.transform.position.x, (obj.transform.position.y + 1f), obj.transform.position.z);

            obj.transform.position = Vector3.Lerp(obj.transform.position, target, speed * Time.deltaTime);

            currentHeight++;

            yield return null;
        }

        Debug.Log("Done lifting that cube boss.");
        
    }
}
