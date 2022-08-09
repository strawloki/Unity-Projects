using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMovement : MonoBehaviour
{
    Transform m_Cam;
    public float sensitivity = 5f;

    float xCamRot, yCamRot;

    // Start is called before the first frame update
    void Start()
    {
        m_Cam = Camera.main.transform;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        float y = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        xCamRot -= y;
        yCamRot -= x;

        m_Cam.localRotation = Quaternion.Euler(xCamRot, 0f, 0f);
        transform.localRotation = Quaternion.Euler(0f, -yCamRot, 0f);

    }
}
