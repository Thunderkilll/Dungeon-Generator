using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public Vector3 offset;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

    }
    void Update()
    {
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player").transform;

        transform.position = new Vector3(player.position.x + offset.x, player.position.y + offset.y, offset.z); // Camera follows the player with specified offset position
    }
}
