using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    public float timeToDestroy;
    // Start is called before the first frame update
    void Awake()
    {
        Destroy(this, timeToDestroy);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
