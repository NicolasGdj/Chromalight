using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public PlayerController player;
    public float distance = 1.0f;

    void Start()
    {
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        if (distanceToPlayer <= distance) {
            player.SetCheckpoint(this);
        }
    }
}
