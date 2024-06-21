using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ChangeLevel()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        string nextScene = "";
        switch (currentScene)
        {
            case "Level One":
                nextScene = "Level Two";
                SceneManager.LoadScene(nextScene);
                break;

            case "Level Two":
                nextScene = "Win";
                SceneManager.LoadScene(nextScene);
                break;

            default:
                nextScene = "Main Menu";
                    break;
        }
    }
}
