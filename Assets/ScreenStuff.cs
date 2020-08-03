using Completed;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScreenStuff : MonoBehaviour
{
    public GameObject TitleScreen;
    public GameObject GameOver;
    public GameObject HUD;
    private int currentHealth;                            //Used to store player food points total during level.
    public Slider slider;
    
    public void ReturntoStart()
    {
        HUD.SetActive(false);
        GameOver.SetActive(false);
        TitleScreen.SetActive(true);
        //Invoke the Restart function to start the next level with a delay of restartLevelDelay (default 1 second).
        
    }

    public void Begin()
    {
        TitleScreen.SetActive(false);
        HUD.SetActive(true);
        SceneManager.LoadScene(0);
    }



}
