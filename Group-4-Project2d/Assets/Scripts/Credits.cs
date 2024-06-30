using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Credits : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("Back", 25.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Returning to the main menu!");
            SceneManager.LoadScene("MainMenu");
        }

    }
void Back()
    {
        Debug.Log("Returning to the main menu!");
       SceneManager.LoadScene("MainMenu");
    }

}
