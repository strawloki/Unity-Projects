using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMovement : MonoBehaviour
{
    public float sensitivity = 25f;
    Transform camera, cTarget;

    float xCamRotation, yCamRotation;

    //attach camera to player and move it and the player to set direction

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        camera = Camera.main.transform;
        
        
    }

    // Update is called once per frame

    
    void Update()
    {
        //CameraFollow();

        float x = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        float y = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;


        yCamRotation -= x;

        xCamRotation -= y;

        transform.localRotation = Quaternion.Euler(0f, -yCamRotation, 0f);
        camera.transform.localRotation = Quaternion.Euler(xCamRotation, -yCamRotation, 0f);
    }
}
