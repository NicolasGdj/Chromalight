using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorController : LaserSender, ILaserActivable
{
    private bool activated = false;

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
        SetColor(sender.GetColor());
        Vector2 direction = Vector2.Reflect(sender.GetDirection(), hit.normal) ;
        DrawLaser(hit.point, direction, GetDistance(), sender.GetBounceCount() + 1);
    
    }

    public void onLaserDeactivated(LaserSender sender) {
        DrawLaser(transform.position, sender.transform.right, 0, sender.GetBounceCount() + 1);
    }
}
