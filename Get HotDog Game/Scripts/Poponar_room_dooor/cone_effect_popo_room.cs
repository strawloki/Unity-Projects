using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;


public class cone_effect_popo_room : MonoBehaviour
{
    //To be attached to the trigger cone
    //UPGRADE TO SmoothDamp instead of LERP
    

    Vector3 downPosition = new Vector3(-0.00075f, -0.00787f, -0.0068f);
    Vector3 upPosition = new Vector3(-0.00075f, -0.00787f, -0.00514f);

    Vector3 popoUpPos = new Vector3(0.007706113f, -0.01649321f, 0.038851411f);
    Vector3 popoDnPos = new Vector3(0.007706113f, -0.01649321f, 0.002151411f);

    Vector3 initPlayerPos = new Vector3(49.81936f, 22.809f, 94.50718f);
    Vector3 finalPlayerPos = new Vector3(48.7604f, 22.809f, 95.97539f);

    Camera m_camera;
    public Camera popoCam;

    PlayableDirector audioData;

   

    public float timeToTop = 1.5f;
    public float speed = 0.5f;
    float timeElapsed = 0f;


    GameObject PoponarDoor;
    GameObject Player;
    float timeElapsedPo = 0f;
    public float popoDoorTime = 1f;

    float timeElapsedPlayer = 0f;
    public float PlayerTravelTime = 5f;

    bool top;

    void StartCutscene()
    {
        gameObject.GetComponent<MeshRenderer>().enabled = false;

        if (m_camera.enabled)
        {
            popoCam.enabled = true;
            m_camera.enabled = false;
            Player.GetComponent<item_mouse_interaction>().enabled = false;
        }
    }
    void EndCutscene()
    {
        if (popoCam.enabled)
        {
            popoCam.enabled = false;
            m_camera.enabled = true;
            Player.GetComponent<item_mouse_interaction>().enabled = true;
        }
        gameObject.SetActive(false);
    }

    void Start()
    {

        StartCoroutine(ConeEffect());
        PoponarDoor = GameObject.Find("popobox_door");
        Player = GameObject.Find("Player");
        m_camera = Camera.main;
        audioData = GetComponent<PlayableDirector>();
    }

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







    void OnTriggerEnter(Collider Other)
    {
        if(Other.gameObject.tag == "Player")
        {
            StartCoroutine(DoorUp());
        }
    }

    IEnumerator DoorUp()
    {
        Player.GetComponent<Player>().FreezePlayer();
        StartCutscene();
        audioData.Play();

        while (timeElapsedPo < popoDoorTime)
        {
            timeElapsedPo += Time.deltaTime;
            PoponarDoor.transform.localPosition = Vector3.Lerp(PoponarDoor.transform.localPosition, popoUpPos, timeElapsedPo / popoDoorTime);
            PoponarDoor.GetComponent<BoxCollider>().enabled = false;
            yield return null;
        }
        

        while(timeElapsedPlayer < PlayerTravelTime)
        {
            timeElapsedPlayer += Time.deltaTime;
            Player.transform.localPosition = Vector3.Lerp(initPlayerPos, finalPlayerPos, timeElapsedPlayer / PlayerTravelTime);
            
            yield return null;
        }

        
        yield return new WaitForSeconds(9);
        EndCutscene();
        Player.GetComponent<Player>().UnfreezePlayer();

    }
}
