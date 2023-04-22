using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Logical : ComponentActivable
{
    public LaserActivable[] activables;
    public ComponentActivable target;

    public override void onActivated() {
        Debug.Log(target);
        target.SetActivated(UpdateLogical(activables));
    }

    public override void onDeactivated() {
        Debug.Log(target);
        target.SetActivated(UpdateLogical(activables));
    }

    public abstract bool UpdateLogical(LaserActivable[] activables);

}
