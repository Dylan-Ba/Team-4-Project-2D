using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


public class Lose : MonoBehaviour
{

    public GameObject text;


    void Start()
    {
        text.SetActive(false);
        Invoke("Text", 1.5f);
    }

  
   public void Text()
    {
        text.SetActive(true);
    }

}
