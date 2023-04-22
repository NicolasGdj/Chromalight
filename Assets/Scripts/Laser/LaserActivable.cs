using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LaserActivable : MonoBehaviour, ILaserActivable
{
    public Sprite deactivatedSprite;
    public Sprite activatedSprite;

    private SpriteRenderer spriteRenderer;

    public ComponentActivable[] related;

    private bool activated = false;

    public void SetActivated(LaserSender sender, bool value, RaycastHit2D hit) {
        if(activated != value) {
            activated = value;
            if(activated) {
                onLaserActivated(sender, hit);
            } else {
                onLaserDeactivated(sender);
            }
            foreach(ComponentActivable component in related) {
                component.SetActivated(value);
            }
            spriteRenderer.sprite = value ? activatedSprite : deactivatedSprite;
        }
    }

    public bool isActivated() {
        return activated;
    }

    public void onLaserActivated(LaserSender sender, RaycastHit2D hit) {
        
    }

    public void onLaserDeactivated(LaserSender sender) {
        
    }

    void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = deactivatedSprite;
    }
}
