using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Instance { get; private set; }  // Singleton instance

    [Header("Player Stats")]
    public int maxHealth = 100;
    public int currentHealth;
    public float regenerationRate = 5f;  // Health regenerated per second
    public float timeToRegen = 3f;  // Delay in seconds before regeneration starts after taking damage

    private float lastDamageTime;  // Time when the player last took damage
    private bool isRegenerating = false;

    void Awake()
    {
        // Ensure only one instance of the player stats exists
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);  // Keep this object across scenes
        }
        else
        {
            Destroy(gameObject);  // Destroy duplicate instances
        }
    }

    void Start()
    {
        currentHealth = maxHealth;  // Initialize player health to max
    }

    void Update()
    {
        HandleRegeneration();
    }

    // Method to apply damage to the player
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;  // Reduce current health by damage taken
        lastDamageTime = Time.time;  // Record the time the player took damage
        isRegenerating = false;  // Stop regeneration when taking damage

        Debug.Log("Player took " + damage + " damage. Current health: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();  // Call the Die method if health reaches 0
        }
    }

    // Method to handle health regeneration
    private void HandleRegeneration()
    {
        // Only regenerate health if enough time has passed since last damage and player is not at max health
        if (Time.time >= lastDamageTime + timeToRegen && currentHealth < maxHealth)
        {
            if (!isRegenerating)
            {
                Debug.Log("Health regeneration started.");
                isRegenerating = true;
            }

            // Regenerate health over time
            currentHealth += Mathf.RoundToInt(regenerationRate * Time.deltaTime);

            if (currentHealth > maxHealth)
            {
                currentHealth = maxHealth;  // Cap health to the maxHealth value
            }

            Debug.Log("Player health: " + currentHealth);
        }
    }

    // Method to handle player death
    private void Die()
    {
        Debug.Log("Player has died.");
        // Implement death logic here (e.g., game over screen, respawn, etc.)
    }
}
