using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{

    /*
     * Buttons to add:
     *      -Resume button 
     *      -Restart Button
     * 
     */

    static GameObject resumeButton, resetButton;
    static bool menu_Enabled;

    AudioSource ads;

    public static bool MenuEnabled
    {
        get { return menu_Enabled; }
        set { menu_Enabled = value;  }
    }

    void Start()
    {
        resumeButton = GameObject.Find("resumeButton");
        DisableButton(resumeButton);

        resetButton = GameObject.Find("resetButton");
        DisableButton(resetButton);
        ads = gameObject.GetComponent<AudioSource>();
        if(ads != null) ads.Play();
        
    }

    public static void EnableMenu()
    {
        Cursor.lockState = CursorLockMode.None;
        menu_Enabled = true;
        Time.timeScale = 0f;

        EnableButton(resumeButton);
        EnableButton(resetButton);
    }
    public static void DisableMenu()
    {
        Cursor.lockState = CursorLockMode.Locked;
        menu_Enabled = false;
        Time.timeScale = 1f;

        DisableButton(resumeButton);
        DisableButton(resetButton);
    }

    public static void EnableDeathMenu()
    {
        //button for reseting only
        //after about 5 seconds after death, launch this menu

        Cursor.lockState = CursorLockMode.None;
        menu_Enabled = true;

        EnableButton(resetButton);



    }

    static void EnableButton(GameObject button)
    {
        
        button.GetComponent<Image>().enabled = true;
        button.transform.GetChild(0).GetComponent<Text>().enabled = true;
    }

    static void DisableButton(GameObject button)
    {
        
        button.GetComponent<Image>().enabled = false;
        button.transform.GetChild(0).GetComponent<Text>().enabled = false;
    }

    public void OnButtonClick_dummyBtn()
    {
        
        DisableMenu();
    }

    public void OnButtonClick_restartBtn()
    {
        SceneManager.LoadScene("getHotDog_scene");
        DisableMenu();
    }

    public void OnButtonClick_playBtn()
    {
        SceneManager.LoadScene("getHotDog_scene");
        //ads.Stop();  //apparently it just stops itself :D
    }

    
}
