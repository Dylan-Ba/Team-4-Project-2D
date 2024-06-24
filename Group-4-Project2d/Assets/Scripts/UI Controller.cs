using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UIController : MonoBehaviour
{
    public GameManager gm;

    public GameObject heartOne;
    public GameObject heartTwo;
    public GameObject heartThree;
    public float playerHealth;
    public GameObject[] capturedGhosts;
    public float ghostCaptured;
    private void Start()
    {
        gm = GameObject.Find("Game Manager").GetComponent<GameManager>();
        playerHealth = gm.currentHealth;
    }
    private void Update()
    {
        HandleHealthUi();
        HandleGhostUi();
    }
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
        SceneManager.LoadScene("Mechanics testing");
    }
    public void OnClickHelpButton() // Lexi
    {
        Debug.Log("Help button was clicked");
        SceneManager.LoadScene("HELP Menu");
    }

    private void HandleHealthUi()
    {
        playerHealth = gm.currentHealth;
        if (playerHealth >= 3)
        {

        }
        else if (playerHealth == 2)
        {
            heartThree.SetActive(false);
        }
        else if (playerHealth == 1)
        {
            heartTwo.SetActive(false);
        }
        else if (playerHealth <= 1)
        {
            heartOne.SetActive(false);
        }
    }

    private void HandleGhostUi()
    {
        ghostCaptured = gm.ghostKilled;
        switch (ghostCaptured)
        {
            case 0:

                break; 
            case 1:
                capturedGhosts[0].gameObject.SetActive(true);
                break;  
            case 2:
                capturedGhosts[1].gameObject.SetActive(true);
                break;
            case 3:
                capturedGhosts[2].gameObject.SetActive(true);
                break;
            default:
                break;
        }

    }
}
