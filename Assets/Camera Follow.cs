using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Camera Settings")]
    public Transform target; // The player or object the camera should follow
    public Vector3 offset = new Vector3(0, 0, -10); // Offset to keep the camera behind the target
    public float smoothSpeed = 0.125f; // Speed of camera movement
    public float zoomSpeed = 2f; // Speed at which the camera zooms
    public float minZoom = 5f; // Minimum zoom level
    public float maxZoom = 10f; // Maximum zoom level

    private Camera cam;

    private void Start()
    {
        cam = GetComponent<Camera>();
    }

    private void LateUpdate()
    {
        FollowTarget();
        HandleZoom();
    }

    private void FollowTarget()
    {
        if (target == null)
        {
            Debug.LogWarning("Target is not assigned to the camera.");
            return;
        }

        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }

    private void HandleZoom()
    {
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");

        if (scrollInput != 0)
        {
            float newZoom = Mathf.Clamp(cam.orthographicSize - scrollInput * zoomSpeed, minZoom, maxZoom);
            cam.orthographicSize = newZoom;
        }
    }
}
