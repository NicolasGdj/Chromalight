using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    public GameObject player;
    public bool isActive = true;
    public float distance;

    void Update()
    {
        if (isActive)
        {
            float currentDistance = Vector3.Distance(player.transform.position, transform.position);

            if (currentDistance <= distance) {            
                RotateTowardsPlayer();
            }
        }
    }

    void RotateTowardsPlayer()
    {
        Vector2 direction = player.transform.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime);
    }
}

