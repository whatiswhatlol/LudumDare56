using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    [Header("Enemy Stats")]
    public int maxHealth = 120;
    private int currentHealth;
    public int attackDamage = 15;
    public float attackCooldown = 2f;  // Cooldown between attacks
    private float lastAttackTime = 0f;

    public float attackRange = 2f;

    [Header("Engagement Settings")]
    public float engagementTime = 1f;  // Time required to engage before attacking
    private float timeEngaged = 0f;  // Tracks how long the enemy has been engaged
    private bool isEngaged = false;  // Whether the enemy is currently engaged with a target


    private FellaSpawner spawner;
    void Start()
    {
        currentHealth = maxHealth;
        spawner = FellaSpawner.Instance;
    }

    void Update()
    {
        if (isEngaged)
        {
            timeEngaged += Time.deltaTime;  // Track how long the enemy has been engaged

            // Attack the fella only after the engagement time has passed and the cooldown has elapsed
            if (timeEngaged >= engagementTime && Time.time >= lastAttackTime + attackCooldown)
            {
                AttackClosestFella();  // Attack after the engagement delay
            }
        }
    }

    // Method to apply damage to this enemy
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log(gameObject.name + " took " + damage + " damage. Current health: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            isEngaged = true;  // Enemy is engaged once attacked
        }
    }

    // Attack the closest fella after engagement
    private void AttackClosestFella()
    {
        FellaStats closestFella = FindClosestFella();
        
        if (closestFella != null)
        {
            if (Vector3.Distance(transform.position, closestFella.transform.position) < attackRange)
            {
                GetComponent<Animator>().SetTrigger("Attack");
                Debug.Log(gameObject.name + " attacks " + closestFella.gameObject.name + " for " + attackDamage + " damage.");
                closestFella.TakeDamage(attackDamage);  // Apply damage to the fella
                lastAttackTime = Time.time;  // Reset cooldown timer
            }
        }
        else
        {
            if (Vector3.Distance(transform.position, PlayerStats.Instance.transform.position) < attackRange)
            {
                Debug.Log("Hunting Player");
                PlayerStats.Instance.TakeDamage(attackDamage);  // Apply damage to the fella
                lastAttackTime = Time.time;  // Reset cooldown timer
            }
        }
    }

    // Find the closest fella to attack
    private FellaStats FindClosestFella()
    {
        List<FellaStats> allFellas = spawner.fellaStats;
        FellaStats closestFella = null;
        float shortestDistance = Mathf.Infinity;

        foreach (FellaStats fella in allFellas)
        {
            if (fella == null) break;
            float distanceToFella = Vector3.Distance(transform.position, fella.transform.position);
            if (distanceToFella < shortestDistance)
            {
                shortestDistance = distanceToFella;
                closestFella = fella;
            }
        }
        if (closestFella != null)
        {
            if (Vector3.Distance(transform.position, closestFella.transform.position) < Vector3.Distance(transform.position, spawner.transform.position))
            {

                return closestFella;
            }
        }
        return null;
    }

    // Method to handle enemy death
    void Die()
    {
        Debug.Log(gameObject.name + " has died.");
        EnemySpawner.Instance.OnEnemyDefeated(this);
        Destroy(gameObject);


        
    }

}
