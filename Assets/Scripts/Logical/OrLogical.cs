using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrLogical : Logical
{

    public override bool UpdateLogical(LaserActivable[] activables) {
        bool value = false;
        foreach(LaserActivable component in activables) {
            value = value || component.isActivated();
        }
        return value;
    }

}
