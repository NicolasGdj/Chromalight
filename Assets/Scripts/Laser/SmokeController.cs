using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeController : LaserSender, ILaserActivable
{
    public bool activated = false;

    public void SetActivated(LaserSender sender, bool value, RaycastHit2D hit) {
        activated = value;
        if(activated) {
            onLaserActivated(sender, hit);
        } else {
            onLaserDeactivated(sender);
        }
    }

    public bool isActivated() {
        return activated;
    }

    public void onLaserActivated(LaserSender sender, RaycastHit2D hit) {
        Vector2 smokePoint = hit.point;
        DrawLaser(smokePoint, new Vector3( smokePoint.x - sender.GetStartPosition().x, smokePoint.y - sender.GetStartPosition().y , 0), GetDistance(), sender.GetBounceCount());
    }

    public void onLaserDeactivated(LaserSender sender) {
        DrawLaser(transform.position, sender.transform.right, 0, sender.GetBounceCount());
    }
}
