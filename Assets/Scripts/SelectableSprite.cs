using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectableSprite : ComponentActivable
{

    public void show() {
        GetSpriteRenderer().enabled = true;
    }

    public void hide() {
        GetSpriteRenderer().enabled = false;
        SetActivated(false);
    }

    public override void onActivated() {
    }

    public override void onDeactivated() {
    }


}
