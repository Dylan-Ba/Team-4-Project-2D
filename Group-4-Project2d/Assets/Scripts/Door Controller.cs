using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    [SerializeField]
    private Collider2D doorCollider;
    [SerializeField]
    private GameObject key;
    [SerializeField]
    private GameManager gm;

    [SerializeField]
    private bool isLocked;
    // Start is called before the first frame update
    void Start()
    {
        isLocked = true;
    }

    // Update is called once per frame
    void Update()
    {
        LockedDoor();
        UnlockDoor();
       
    }

    private void LockedDoor()
    {
        if (isLocked == true)
        {
            doorCollider.gameObject.SetActive(true);
        }
        else
        {
            doorCollider.gameObject.SetActive(false);
        }
    }
    private void UnlockDoor()
    {
        if (gm.keyCollected == false)
        {
            isLocked = true;
        }
        else
        {
            isLocked = false;
        }
    }
}
