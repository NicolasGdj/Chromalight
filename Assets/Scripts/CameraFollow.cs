using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public float smoothSpeed = 0.125f;

    public CameraFocus focus;

    public GameObject min;
    public GameObject max;
    private Vector2 minBounds;
    private Vector2 maxBounds;

    private float _cameraHalfWidth;
    private float _cameraHalfHeight;

    private void Start() {
        minBounds = min.transform.position;
        maxBounds = max.transform.position;
        _cameraHalfHeight = Camera.main.orthographicSize;
        _cameraHalfWidth = _cameraHalfHeight * Camera.main.aspect;
    }

    private void LateUpdate() {
        if(focus != null) {
            FollowFocus();
        } else {
            FollowPlayer();
        }
    }

    private void FollowFocus()
    {
        Camera mainCamera = Camera.main;

        Vector3 minPoint = focus.GetMin();
        Vector3 maxPoint = focus.GetMax();
        Vector3 centerPoint = (minPoint + maxPoint) / 2;

        float width = maxPoint.x - minPoint.x;
        float height = maxPoint.y - minPoint.y;
        
        float orthographicSize = Mathf.Max(width / mainCamera.aspect, height) / 2;

        float margin = 1.1f;
        mainCamera.orthographicSize = Mathf.Lerp(mainCamera.orthographicSize, orthographicSize * margin, 2.0f * Time.deltaTime);

        Vector3 targetPosition = new Vector3(centerPoint.x, centerPoint.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, targetPosition, 2.0f * Time.deltaTime);
    }

    private void FollowPlayer() {
        if (player != null) {
            Vector3 desiredPosition = new Vector3(player.position.x, player.position.y, player.position.z);
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            float clampedX = Mathf.Clamp(smoothedPosition.x, minBounds.x + _cameraHalfWidth, maxBounds.x - _cameraHalfWidth);
            float clampedY = Mathf.Clamp(smoothedPosition.y, minBounds.y + _cameraHalfHeight, maxBounds.y - _cameraHalfHeight);
            transform.position = new Vector3(clampedX, clampedY, transform.position.z);
        }
    }

    public CameraFocus GetFocus() {
        return focus;
    }

    public void SetFocus(CameraFocus focus) {
        this.focus = focus;
        if(focus == null) {
            Camera.main.orthographicSize = 5;
        }
    }
}