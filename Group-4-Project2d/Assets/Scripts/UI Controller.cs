using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UIController : MonoBehaviour
{
    public void OnClickQuitButton() // Lexi
    {
        print("Quit button was clicked");
        Application.Quit();
    }
    public void OnClickBackButton() // Lexi
    {
        Debug.Log("Back button was clicked");
        SceneManager.LoadScene("Main Menu");
    }
    public void OnClickPlayButton() // Lexi
    {
        print("Play button was clicked");
        SceneManager.LoadScene("Level One");
    }
    public void OnClickHelpButton() // Lexi
    {
        Debug.Log("Help button was clicked");
        SceneManager.LoadScene("HELP Menu");
    }
}
