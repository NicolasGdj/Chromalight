using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AndLogical : Logical {

    public override bool UpdateLogical(LaserActivable[] activables) {
        bool value = true;
        foreach(LaserActivable component in activables) {
            value = value && component.isActivated();
        }
        return value;
    }

}
