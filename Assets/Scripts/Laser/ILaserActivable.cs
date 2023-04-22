using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILaserActivable {

    void SetActivated(LaserSender sender, bool value, RaycastHit2D hit);
    bool isActivated();
    void onLaserActivated(LaserSender sender, RaycastHit2D hit);
    void onLaserDeactivated(LaserSender sender);
    
}
