using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class ButtonController : MonoBehaviour
{
    
    //Main Menu Buttons
    public Button play;
    public Button help;
    public Button quit;




    //Button functionality
    public void PlayGame()
    {
        SceneManager.LoadScene("LevelOne");
    }


    public void Help()
    {
        SceneManager.LoadScene("HelpMenu");
    }

    public void QuitGame()
    {
        Debug.Log("Quitting the game!");
        Application.Quit();
    }
}
