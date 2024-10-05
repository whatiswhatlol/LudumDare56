using System.Collections.Generic;
using UnityEngine;

public class FellaStats : MonoBehaviour
{
    [Header("Fella Stats")]
    public int maxHealth = 100;
    private int currentHealth;
    public int attackDamage = 10;
    public float attackCooldown = 1.5f;  // Cooldown between attacks
    private float lastAttackTime = 0f;

    void Start()
    {
        currentHealth = maxHealth;
    }

    void Update()
    {
        // Handle combat against all available enemies
        HandleCombat();
    }

    // Method to apply damage to this Fella
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;  // Reduce health
        Debug.Log(gameObject.name + " took " + damage + " damage. Current health: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();  // Fella dies if health reaches zero
        }
    }

    // Method for this fella to handle combat
    private void HandleCombat()
    {
        EnemyStats targetEnemy = FindClosestEnemy();  // Find the nearest enemy

        if (targetEnemy != null && Time.time >= lastAttackTime + attackCooldown)
        {
            Attack(targetEnemy);
        }
    }

    // Find the closest enemy
    private EnemyStats FindClosestEnemy()
    {
        List<EnemyStats> allEnemies = EnemySpawner.Instance.enemyStats;
        EnemyStats closestEnemy = null;
        float shortestDistance = Mathf.Infinity;

        // Loop through all enemies to find the closest one
        foreach (EnemyStats enemy in allEnemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                closestEnemy = enemy;
            }
        }

        return closestEnemy;
    }

    // Simple attack method that applies damage to an enemy
    private void Attack(EnemyStats targetEnemy)
    {
        if (targetEnemy != null)
        {
            Debug.Log(gameObject.name + " attacks " + targetEnemy.gameObject.name + " for " + attackDamage + " damage.");
            targetEnemy.TakeDamage(attackDamage);  // Apply damage to the enemy
            lastAttackTime = Time.time;  // Reset cooldown timer
        }
    }

    // Method to handle what happens when this Fella dies
    private void Die()
    {
        Debug.Log(gameObject.name + " has died.");
        Destroy(gameObject);  // Destroy the fella
    }
}
