using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ComponentActivable : MonoBehaviour
{
    
    public Sprite deactivatedSprite;
    public Sprite activatedSprite;

    private SpriteRenderer spriteRenderer;

    public bool activated = false;
    private bool lastValue = false;

    public void SetActivated(bool value) {
        if(lastValue != value) {
            lastValue = activated;
            activated = value;

            if(value) {
                onActivated();
            } else {
                onDeactivated();
            }
            if(spriteRenderer)
                spriteRenderer.sprite = value ? activatedSprite : deactivatedSprite;
        }
    }

    public bool isActivated() {
        return activated;
    }

    public SpriteRenderer GetSpriteRenderer() {
        return spriteRenderer;
    }

    public abstract void onActivated();
    public abstract void onDeactivated();

    public void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if(spriteRenderer)
            spriteRenderer.sprite = deactivatedSprite;
    }


    public void Update() {
        if(lastValue != activated) {
            SetActivated(activated);
        }
    }
}
