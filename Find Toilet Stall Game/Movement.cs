using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    CharacterController cController;
    Vector3 velocity;
    GameObject gndCheck;
    public LayerMask groundMask;

    bool isGrounded;
    float horiontal, vertical;

    public float moveSpeed = 10f;
    public float gravity = -9.81f, weight = 85f;
    public float jumpHeight = 5f;
    float gravityConstant = -9.81f;


    // Start is called before the first frame update
    void Awake()
    {
        gndCheck = GameObject.Find("gndCheck");
        cController = gameObject.GetComponent<CharacterController>();
        //also add groundCheck Sphere Game OBject refference here

    }

    // Update is called once per frame
    void Update()
    {

        horiontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        isGrounded = Physics.CheckSphere(gndCheck.transform.position, 0.2f, groundMask);

        if(isGrounded && velocity.y < 0)
        {
            velocity.y = 0f; 
        }

        Vector3 move = transform.right * horiontal + transform.forward * vertical;

        cController.Move(move * Time.deltaTime * moveSpeed);

        velocity.y += gravity * weight * Time.deltaTime;

        cController.Move(velocity * Time.deltaTime);

       //check if is grounded with Physics.CheckSphere   


       if(Input.GetKeyDown("space") && isGrounded)
       {
           
           velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravityConstant); 
       }
    }
}
