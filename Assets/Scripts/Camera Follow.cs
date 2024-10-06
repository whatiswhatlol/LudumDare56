using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Camera Settings")]
    public Transform playerTransform;  // The player or object the camera should follow
    public float smoothTime = 0.3f;    // Adjust this value to make the camera follow smoother (smaller value = smoother but slower follow)
    public float followDelay = 0.1f;   // The delay before the camera catches up to the player (bigger value = more delay)

    private Vector3 velocity = Vector3.zero;  // Used for SmoothDamp

    void LateUpdate()
    {
        if (playerTransform != null)
        {
            // Calculate the target position based on the player's position but keep the camera's z position unchanged
            Vector3 targetPosition = new Vector3(playerTransform.position.x, playerTransform.position.y, transform.position.z);

            // Only move the camera if the player has moved beyond the follow delay threshold
            float distance = Vector2.Distance(new Vector2(transform.position.x, transform.position.y), new Vector2(playerTransform.position.x, playerTransform.position.y));
            if (distance > followDelay)
            {
                // Smoothly move the camera towards the player's position with a delay
                transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
            }
        }
    }
}
