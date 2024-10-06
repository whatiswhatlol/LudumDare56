using UnityEngine;

public class EnemyBoundsChecker : MonoBehaviour
{
    private EnemySpawner spawner;  // Reference to the spawner to re-randomize position

    void Start()
    {
        spawner = EnemySpawner.Instance;
    }

    // Detect when the enemy enters an out-of-bounds trigger zone
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("OutOfBounds"))  // Assuming out-of-bounds colliders are tagged "OutOfBounds"
        {
            Debug.Log("Enemy spawned out of bounds, repositioning...");
            RepositionEnemy();
        }
    }

    // Method to move the enemy to a valid in-bounds position
    private void RepositionEnemy()
    {
        Vector3 newPosition = spawner.GetRandomOffScreenPosition();  // Reuse the spawner's position generation method
        transform.position = newPosition;  // Move the enemy to the new valid position
    }
}
