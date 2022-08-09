using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float hungerRate = 0.1f;
    public float sprintHungerMultiplier = 1.3f;
    float horizontal;
    float vertical;


   
    public Vector3 prefabSpawnOffset;

    CharacterController charControl;
    Player player;

    public float speed = 15f;
    public float sprintMultiplier = 1.5f;
    public float jumpHeight = 5f;

    public LayerMask groundMask;
    Transform groundCheck;
    public float groundDistance = 0.4f;
    public float gravityConstant = -9.81f;
    bool isGrounded = false;
    bool isSprinting;
    bool isMoving;
   

    Vector3 velocity;

    // Start is called before the first frame update
    void Awake()
    {
        charControl = gameObject.GetComponent(typeof(CharacterController)) as CharacterController;
        groundCheck = GameObject.Find("GroundCheck").transform;
        //player = gameObject.GetComponent(typeof(Player)) as Player;
    }

    // Update is called once per frame
    void Update()
    {
        player = gameObject.GetComponent(typeof(Player)) as Player;

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(isGrounded && velocity.y < 0)
        {
            velocity.y = 0f;
        }

        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        
        if (horizontal <= 0f && vertical <= 0f)
        {
            isMoving = false;
            
        }
        else isMoving = true;
        
        Vector3 move = transform.right * Input.GetAxis("Horizontal") + transform.forward * Input.GetAxis("Vertical");

        charControl.Move(move * Time.deltaTime * speed);

        velocity.y += gravityConstant * Time.deltaTime;

        charControl.Move(velocity * Time.deltaTime);

        if(player.IsPlayerHungry())
        {
           
           
            player.onDeath();
            //gameObject.GetComponent<Movement>().enabled = false;
        }

        if (isMoving)
        {
            //Debug.Log((hungerRate * Time.deltaTime) * sprintHungerMultiplier);  

            if (isSprinting) player.AddPlayerHunger(hungerRate * sprintHungerMultiplier * Time.deltaTime);

            
          

            else player.AddPlayerHunger(hungerRate * Time.deltaTime);
           
        }

        if (Input.GetKeyDown("space") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravityConstant); //*weight
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            isSprinting = true;
            speed = speed * sprintMultiplier;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            isSprinting = false;
            speed = speed / sprintMultiplier;
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (!ButtonManager.MenuEnabled) { ButtonManager.EnableMenu(); }
            else { ButtonManager.DisableMenu(); }
        }
        /*
        if(Input.GetKeyDown(KeyCode.G))
        {
            
            Instantiate(prefab, transform.position + prefabSpawnOffset, transform.localRotation);
        }
        */

    }
}
