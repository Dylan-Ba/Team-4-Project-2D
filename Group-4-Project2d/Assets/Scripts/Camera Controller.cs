using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public GameObject player;
    public float offsetH;
    public float offsetV;
    public float offsetSmoothing;
    private Vector3 playerPosition;

    private void Start()
    {
        transform.position = new Vector3 (player.transform.position.x + offsetH, transform.position.y + offsetV, -10);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        playerPosition = new Vector3 (player.transform.position.x, player.transform.position.y + offsetV, transform.position.z);

        if (player.transform.localScale.x > 0)
        {
            playerPosition = new Vector3(playerPosition.x + offsetH, player.transform.position.y + offsetV, transform.position.z);
        }
        else
        {
            playerPosition = new Vector3(playerPosition.x - offsetH, player.transform.position.y + offsetV, transform.position.z);
        }

        transform.position = Vector3.Lerp(transform.position, playerPosition, offsetSmoothing * Time.deltaTime);
    }
}
