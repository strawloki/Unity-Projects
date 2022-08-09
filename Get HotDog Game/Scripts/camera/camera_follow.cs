using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera_follow : MonoBehaviour
{
    Transform target;
    public Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("head").transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 desiredPosition = target.transform.position;

        transform.position = desiredPosition;
    }
}
