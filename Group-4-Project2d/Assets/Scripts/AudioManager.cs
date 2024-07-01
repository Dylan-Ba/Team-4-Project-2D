

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



//Tutorial I used https://www.youtube.com/watch?v=QuXqyHpquLg
//There's an Audio Manager prefab in the prefabs folder


public class AudioManager : MonoBehaviour
{
    //Audio
    public static AudioManager Instance;  //Creates a singleton

    [SerializeField] private AudioSource audioSource, musicSource, ambientSource;   //Requires three Audio Sources in the scene - one for sound effects, music, and ambience.
    [SerializeField] private AudioClip bark, birds, ghost, growl, hit, jump, wind, doorOpen, keyPickup, playerDie, swing, spikes, unlock, wolfDie, ghostDie;


    [Header("Background Music")]  //Arrays that let us choose between multiple music clips if we want to have something different for the win and lose screens etc.
    [SerializeField] private AudioClip[] bgMusic;
    [Header("Ambient Sounds")]
    [SerializeField] private AudioClip[] ambientSounds;


    private int clipIndex;

    private void Awake()  //Prevents accidental duplication of AudioManager
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }


    void Start()
    {

        BackgroundMusic(0);

        Ambience();
    }

    private void Ambience()  //Randomly chooses one of the ambient sounds to play at an interval between 4-10 seconds
    {
        clipIndex = Random.Range(0, ambientSounds.Length - 1);
        ambientSource.clip = ambientSounds[clipIndex];
        ambientSource.PlayDelayed(Random.Range(4f, 10f));
        Debug.Log("playing ambient sound!");
    }


    public void BackgroundMusic(int sceneNumber)
    {
        musicSource.clip = bgMusic[sceneNumber];
        musicSource.Play();
    }


    public void Bark()
    {
        audioSource.clip = bark;
        audioSource.Play();
    }

    public void Growl()
    {
        audioSource.clip = growl;
        audioSource.Play();
    }
    public void Ghost()
    {
        audioSource.clip = ghost;
        audioSource.Play();
    }
    public void Hit()
    {
        audioSource.clip = hit;
        audioSource.Play();
    }
    public void Jump()
    {
        audioSource.clip = jump;
        audioSource.Play();
    }


    public void DoorOpen()
    {
        audioSource.clip = doorOpen;
        audioSource.Play();
    }


    public void KeyPickup()
    {
        audioSource.clip = keyPickup;
        audioSource.Play();
    }


    public void PlayerDie()
    {
        audioSource.clip = playerDie;
        audioSource.Play();
    }

    public void Swing()
    {
        audioSource.clip = swing;
        audioSource.Play();
    }

    public void Spikes()
    {
        audioSource.clip = spikes;
        audioSource.Play();
    }

    public void Unlock()
    {
        audioSource.clip = unlock;
        audioSource.Play();
    }

    public void GhostDie()
    {
        audioSource.clip = ghostDie;
        audioSource.Play();
    }

    public void WolfDie()
    {
        audioSource.clip = wolfDie;
        audioSource.Play();
    }

    //Ambient sounds

    public void Birds()
    {
        audioSource.clip = birds;
        audioSource.Play();
    }
    public void Wind()
    {
        audioSource.clip = wind;
        audioSource.Play();
    }

   
    //To use the AudioManager in another script, just use
    // AudioManager.Instance.MySound();



}
