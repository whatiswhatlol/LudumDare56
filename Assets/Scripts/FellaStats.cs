using Pathfinding;
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
    private float attackDistance = 0.5f;
    public float lifeSpan = 60f;  // Lifespan in seconds

    private AIDestinationSetter destination;

    void Start()
    {
        currentHealth = maxHealth;
        destination = GetComponent<AIDestinationSetter>();
    }

    void Update()
    {
        // Handle lifespan countdown and die if it reaches zero
        HandleLifespan();

        // Handle combat against all available enemies
        HandleCombat();

        if (PlayerStats.Instance.isdead)
        {
            Die();
        }
    }

    // Method to handle lifespan countdown
    private void HandleLifespan()
    {
        lifeSpan -= Time.deltaTime;  // Reduce lifespan over time

        if (lifeSpan <= 0f)
        {
            Die();  // Fella dies when lifespan reaches zero
        }
    }

    // Method to apply damage to this Fella
    public void TakeDamage(int damage)
    {
        Debug.Log("hurt fella");
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
        if (targetEnemy != null && Time.time >= lastAttackTime + attackCooldown && attackDistance <= Vector3.Distance(transform.position, targetEnemy.transform.position))
        {
            Debug.Log(targetEnemy.gameObject.name);

            destination.target = targetEnemy.gameObject.transform;
            Attack(targetEnemy);
            Debug.Log("ATTACK");
        }
    }

    // Find the closest enemy
    private EnemyStats FindClosestEnemy()
    {
        List<EnemyStats> allEnemies = EnemySpawner.Instance.enemyStats;
        EnemyStats closestEnemy = null;
        float shortestDistance = Mathf.Infinity;
        Debug.Log("1");

        // Loop through all enemies to find the closest one
        foreach (EnemyStats enemy in allEnemies)
        {
            if (enemy == null) break;

            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                closestEnemy = enemy;
            }
        }
        Debug.Log("2");

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
        FellaSpawner.Instance.fellaStats.Remove(this);  // Remove this fella from the spawner's list
        Destroy(gameObject);  // Destroy the fella object
    }
}
