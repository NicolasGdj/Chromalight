using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prism : LaserActivable {

    public LaserEmitterController[] emitters;
    public Color color = Color.white;

    public override void onLaserActivated(LaserSender sender, RaycastHit2D hit) {
        if (sender.GetColor() == color) {
            foreach(LaserEmitterController emitter in emitters) {
                emitter.enabled = true;
            }
        } else {
            onLaserDeactivated(sender);
        }
    }

    public override void onLaserDeactivated(LaserSender sender) {
        foreach(LaserEmitterController emitter in emitters) {
            emitter.enabled = false;
        }
    }

}
