using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Linq;

public class LevelEditor : MonoBehaviour
{

    public PlayerController player;
    public Tilemap validPositions;
    public float workstationProximityThreshold = 0.5f;
    public CameraFollow camera;
    public CameraFocus focus;
    public GameObject[] selectionableObjects;
    public Workstation[] workstations;
    public LaserEmitterController[] laserSenders;
    
    private KeyCode toggleEditModeKey = KeyCode.E;
    private bool isEditing = false;
    private GameObject selectedObject;
    private int selectedObjectColliderCount = 0;
    private float gridSize = 0.64f;
    private float radius = 0.25f;
    private TilemapRenderer validPositionsRenderer;

    void Start() {
        validPositionsRenderer = validPositions.GetComponent<TilemapRenderer>();
        validPositionsRenderer.enabled = false;
    }

    void Update() {
        if (Input.GetKeyDown(toggleEditModeKey)) {
            if(!isEditing) {
                bool found = false;
                Vector3 center = player.transform.position;

                foreach (Workstation workstation in workstations) {
                    Vector3 position = workstation.transform.position;
                    float distance = Vector3.Distance(center, position);
                    if (distance <= workstationProximityThreshold) {
                        found = true;
                        break;
                    }
                }

                if(!found)
                    return;
                camera.SetFocus(focus);
            } else {
                camera.SetFocus(null);
                selectedObject = null;
            }
            isEditing = !isEditing;
            player.SetEditing(isEditing);
            foreach(LaserEmitterController sender in laserSenders) {
                sender.enabled = !isEditing;
            }
            foreach(GameObject obj in selectionableObjects) {
                SelectableSprite sprite = obj.GetComponentInChildren<SelectableSprite>();
                if(sprite != null) {
                    if(isEditing)
                        sprite.show();
                    else
                        sprite.hide();
                }
            }
            validPositionsRenderer.enabled = isEditing;
        }

        if (isEditing) {
            EditMode();
        }
    }


    private bool IsEditableObject(GameObject obj) {
        foreach (GameObject editableObject in selectionableObjects) {
            if (obj == editableObject) {
                return true;
            }
        }

        return false;
    }

    void SetSelectedObject(GameObject obj) {
        if(selectedObject != null) {
            SelectableSprite sprite = selectedObject.GetComponentInChildren<SelectableSprite>();
            if(sprite != null) {
                sprite.SetActivated(false);
            }
        }
        selectedObject = obj;
        if(selectedObject != null) {
            SelectableSprite sprite = selectedObject.GetComponentInChildren<SelectableSprite>();
            if(sprite != null) {
                sprite.SetActivated(true);
            }
        }
    }

    void EditMode() {
        if (Input.GetMouseButtonDown(0)) {
            if(selectedObject != null) {
                SetSelectedObject(null);
            } else {
                RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
                if(hit.collider != null) {
                    GameObject hitObject = hit.collider.gameObject;
                    if (IsEditableObject(hitObject)) {
                        SetSelectedObject(hitObject);
                        selectedObjectColliderCount = GetColliderCount(SnapToGrid(selectedObject.transform.position), selectedObject);
                    } else {
                        SetSelectedObject(null);
                    }
                } else {
                    SetSelectedObject(null);
                }
            }
        }

        if (selectedObject != null) {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 gridPosition = SnapToGrid(mousePosition);
            if(validPositions.GetTile(validPositions.WorldToCell(mousePosition)) != null && GetColliderCount(gridPosition, selectedObject) == 0) {
                selectedObject.transform.position = new Vector3(gridPosition.x, gridPosition.y, selectedObject.transform.position.z);
            }
            float scrollDelta = Input.GetAxis("Mouse ScrollWheel");
            
            if (scrollDelta > 0) {
                Quaternion rotation = Quaternion.Euler(0, 0, 90f);
                selectedObject.transform.rotation = selectedObject.transform.rotation * rotation;
            } else if (scrollDelta < 0) {
                Quaternion rotation = Quaternion.Euler(0, 0, -90f);
                selectedObject.transform.rotation = selectedObject.transform.rotation * rotation;
            }
        }
    }

    int GetColliderCount(Vector2 position, GameObject exclude) {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(position, radius);
        
        // Filtrer les collisions pour exclure l'objet spécifié et ses enfants
        Collider2D[] filteredColliders = colliders.Where(collider =>
            collider.gameObject != exclude &&
            !collider.transform.IsChildOf(exclude.transform)
        ).ToArray();
        
        return filteredColliders.Length;
    }

    bool IsAnEmptyCell(Vector2 position, GameObject ignoreObject) {
        RaycastHit2D[] hits = Physics2D.RaycastAll(position, Vector2.zero);
        foreach (RaycastHit2D hit in hits) {
            if (hit.collider != null && hit.collider.gameObject != ignoreObject && IsEditableObject(hit.collider.gameObject)) {
                return false;
            }
        }
        return true;
    }

    Vector2 SnapToGrid(Vector3 position)
    {
        float x = Mathf.Round(position.x / gridSize) * gridSize;
        float y = Mathf.Round(position.y / gridSize) * gridSize;
        return new Vector2(x, y);
    }
}
