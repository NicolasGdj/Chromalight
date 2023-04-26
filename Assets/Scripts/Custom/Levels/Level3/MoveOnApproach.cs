using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveOnApproach : MonoBehaviour
{
    private Vector3 from;
    public Vector3 to;
    public bool relative;

    public GameObject detector;
    public float distance;
    
    public GameObject player;
    
    public float moveSpeed = 5.0f;
    public float returnSpeed = 1.0f;

    void Start() {
        from = transform.position;
        if (relative) {
            to = from + to;
        }
    }

    void Update() {
        float currentDistance = Vector3.Distance(player.transform.position, detector.transform.position);

        if (currentDistance <= distance) {
            MoveTowardsTarget(to, moveSpeed);
        } else {
            MoveTowardsTarget(from, returnSpeed);
        }
    }

    void MoveTowardsTarget(Vector3 targetPosition, float speed) {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
    }
}
