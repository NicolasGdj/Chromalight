using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : ComponentActivable
{
    private Collider2D collider;
    
    public void Start()
    {
        base.Start();
        collider = GetComponent<Collider2D>();

    }

    public void Update()
    {
        base.Update();
    }

    public override void onActivated() {
        collider.enabled = false; 
    }

    public override void onDeactivated() {
        collider.enabled = true; 
    }

}
