using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserEmitterController : LaserSender
{
    public bool enabled = true;
    void Update()
    {
        DrawLaser(transform.position + transform.right * 0.1f, transform.right, enabled ? GetDistance() : 0, 0);
    }

}
