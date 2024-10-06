using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image healthFillImage;  // The fill image representing the player's health
    PlayerStats playerStats;  // Reference to the PlayerStats script

    private void Start()
    {
        // Initialize the health bar to the player's current health
        playerStats = PlayerStats.Instance;
        SetMaxHealth(playerStats.maxHealth);
    }

    private void Update()
    {
        // Continuously update the health bar as the player's health changes
        SetHealth(playerStats.currentHealth);
    }

    // Set the maximum value of the health bar
    public void SetMaxHealth(int health)
    {
        healthFillImage.fillAmount = 1f;  // Set the fill amount to full
    }

    // Update the health bar based on the current health
    public void SetHealth(int health)
    {
        // Calculate the fill amount as a percentage of the max health
        float healthPercent = (float)health / playerStats.maxHealth;
        healthFillImage.fillAmount = healthPercent;  // Update the image's fill amount
    }
}
