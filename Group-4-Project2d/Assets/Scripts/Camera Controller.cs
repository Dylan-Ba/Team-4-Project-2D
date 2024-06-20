using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public GameObject player;
    public float offset;
    public float offsetSmoothing;
    private Vector3 playerPosition;

    // Update is called once per frame
    void FixedUpdate()
    {
        playerPosition = new Vector3 (player.transform.position.x, player.transform.position.y + 1f, transform.position.z);

        if (player.transform.localScale.x > 0)
        {
            playerPosition = new Vector3(playerPosition.x + offset, player.transform.position.y + 1f, transform.position.z);
        }
        else
        {
            playerPosition = new Vector3(playerPosition.x - offset, player.transform.position.y + 1f, transform.position.z);
        }

        transform.position = Vector3.Lerp(transform.position, playerPosition, offsetSmoothing * Time.deltaTime);
    }
}
