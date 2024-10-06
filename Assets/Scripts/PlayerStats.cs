using UnityEngine;
using UnityEngine.UI;

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
    private float healthRegenAccumulator = 0f;  // To accumulate fractional health over time

    public GameObject LivingCanvas;
    public GameObject DeathCanvas;

    public bool isdead = false;
    public AudioSource hurt;
    public AudioSource die;
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
        hurt.Play();
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
        // Only regenerate health if enough time has passed since the last damage and player is not at max health
        if (Time.time >= lastDamageTime + timeToRegen && currentHealth < maxHealth && !isdead)
        {
            if (!isRegenerating)
            {
                Debug.Log("Health regeneration started.");
                isRegenerating = true;
            }

            // Accumulate the regeneration amount over time
            healthRegenAccumulator += regenerationRate * Time.deltaTime;

            // Only apply regeneration when enough health is accumulated (i.e., 1 full health point)
            if (healthRegenAccumulator >= 1f)
            {
                int regenAmount = Mathf.FloorToInt(healthRegenAccumulator);  // Get the integer part
                currentHealth = Mathf.Min(currentHealth + regenAmount, maxHealth);  // Increase health but cap at maxHealth
                healthRegenAccumulator -= regenAmount;  // Subtract the applied health from the accumulator

                Debug.Log("Player health: " + currentHealth);
            }
        }
        else
        {
            isRegenerating = false;  // Reset the regenerating flag when not regenerating
        }
    }


    // Method to handle player death
    private void Die()
    {
        die.Play();
        Debug.Log("Player has died.");
        LivingCanvas.SetActive(false);
        DeathCanvas.SetActive(true);
        isdead = true;

        // Implement death logic here (e.g., game over screen, respawn, etc.)
    }
}
