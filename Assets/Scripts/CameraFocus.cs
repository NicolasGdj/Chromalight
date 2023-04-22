using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFocus : MonoBehaviour
{
    public GameObject min;
    public GameObject max;

    public Vector3 GetMin() {
        return min.transform.position;
    }

    public Vector3 GetMax() {
        return max.transform.position;
    }

}
