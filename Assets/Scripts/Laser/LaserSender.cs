using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LaserSender : MonoBehaviour
{
    static public int MAX_BOUNCES = 10;

    public Color color = Color.white;
    public float distance = 100.0f;
    public LayerMask laserHitLayers;
    public Texture2D beamTexture;
    private LineRenderer lineRenderer;
    private ArrayList colliders;

    public Vector2 startPosition;
    public Vector3 direction;
    public float colliderDistance = 0.1f;
    private int bounces = 0;

    void Start() {
        colliders = new ArrayList(); 
        lineRenderer = GetComponent<LineRenderer>();
        if(lineRenderer == null) {
            lineRenderer = gameObject.AddComponent<LineRenderer>();
            lineRenderer.startWidth = 0.6f;
            lineRenderer.endWidth = 0.6f;
            lineRenderer.sortingOrder = -1;
            lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
            lineRenderer.material.mainTexture = beamTexture;
            lineRenderer.startColor = GetColor();
            lineRenderer.endColor =  GetColor();
        }
    }

    public Color GetColor() {
        return color;
    }

    public void SetColor(Color color) {
        this.color = color;
        lineRenderer.startColor = color;
        lineRenderer.endColor = color;
    }

    public float GetDistance() {
        return distance;
    }
    
    public Vector3 GetDirection() {
        return direction;
    }
    
    public Vector2 GetStartPosition() {
        return startPosition;
    }

    public int GetBounceCount() {
        return bounces;
    }

    public void SetBouceCount(int bounce) {
        this.bounces = bounce;
    }

    public RaycastHit2D[] GetHits(Vector2 startPosition, Vector3 direction, float distance) {
        return Physics2D.RaycastAll(startPosition + new Vector2(direction.x, direction.y).normalized * colliderDistance, direction, distance, laserHitLayers);
    }

    public void DrawLaser(Vector2 startPosition, Vector3 direction, float distance, int bounce) {
        
        if(distance <= 0.5) {
            lineRenderer.enabled = false;
            return;
        } else {
            lineRenderer.enabled = true;
        }

        if(bounce > MAX_BOUNCES)
            return;
        
        SetBouceCount(bounce);

        direction = direction.normalized;

        this.direction = direction;
        this.startPosition = startPosition;
        lineRenderer.SetPosition(0, startPosition);

        RaycastHit2D[] hits = GetHits(startPosition, direction, distance);

        if( hits.Length == 0 ) {
            lineRenderer.SetPosition(1, transform.position + direction * distance);
        }

        ArrayList tmpColliders = new ArrayList();
        foreach (RaycastHit2D hit in hits) {
            bool blocked = false;

            if (hit.collider != null) {
                
                if (!hit.collider.CompareTag("Transparent")) {
                    GlassController glass = hit.collider.GetComponent<GlassController>();
                    if (glass == null || color != glass.GetColor()) {
                        blocked = true;
                    }
                } 
                
                ILaserActivable laserActivable = hit.collider.GetComponent<ILaserActivable>();
                if (laserActivable != null && laserActivable != this) {
                    tmpColliders.Add(laserActivable);
                    laserActivable.SetActivated(this, true, hit);
                }
                         
                
                if (hit.collider.CompareTag("Player")) { 
                    PlayerController player = hit.collider.GetComponent<PlayerController>();
                    if (player != null) {
                        if(player.GetCheckpoint() != null) {
                            player.transform.position = player.GetCheckpoint().transform.position;
                        }
                    }
                }


            }
            if(blocked) {
                lineRenderer.SetPosition(1, hit.point);
                break;
            } else {
                lineRenderer.SetPosition(1, transform.position + direction * distance);
            }
        }


        foreach (ILaserActivable laserActivable in colliders) {
            if(laserActivable != null && !tmpColliders.Contains(laserActivable)) {
                laserActivable.SetActivated(this, false, default(RaycastHit2D));
            }
        }
        colliders = tmpColliders;

 
    }

}
