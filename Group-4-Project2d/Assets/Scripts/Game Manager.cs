using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth;

    public static GameManager instance;
    private void Awake()

    {
        maxHealth = 3;
        currentHealth = maxHealth;
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
        if (currentHealth <= 0)
        {
            HandleDeath();
        }
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
    private void HandleDeath()
    {
        Debug.Log("Death!!!");
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
        currentHealth = maxHealth;
    }
}
