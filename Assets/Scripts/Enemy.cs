using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Enemy : MonoBehaviour, IDamageable
{
    [Header("Enemy Settings")]
    public float currentHealth = 100f;
    [SerializeField]
    private float maximumHealth = 100f;
    public float speed = 2f;
    public float attackDamage = 10f; // Damage dealt by the enemy

    [SerializeField]
    private Image healthBar;

    [Header("NavMesh Settings")]
    public NavMeshAgent navMeshAgent; // Reference to the NavMeshAgent component
    public Transform player; // Reference to the player transform
    public LayerMask whatIsGround, whatIsPlayer; // Layers for ground and player

    [Header("Patrol Settings")]
    public Vector3 walkPoint; // The point the enemy will patrol to
    bool walkPointSet; // Whether the patrol point is set or not
    public float walkPointRange; // Range within which the enemy can patrol

    [Header("Attack Settings")]
    public float timeBetweenAttacks; // Time between attacks
    bool alreadyAttacked; // Whether the enemy has already attacked or not

    [Header("Enemy States")]
    public float sightRange, attackRange; // Ranges for sight and attack
    public bool playerInSightRange, playerInAttackRange; // Whether the player is in sight or attack range

    [Header("Debug Settings")]
    [SerializeField]
    private bool invokeDamage;
    private int damageAmount = 10;

    private void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer); // Check if the player is in sight range
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer); // Check if the player is in attack range

        if (!playerInSightRange && !playerInAttackRange) Patroling(); // If the player is not in sight or attack range, patrol
        if (playerInSightRange && !playerInAttackRange) ChasePlayer(); // If the player is in sight range but not attack range, chase the player
        if (playerInAttackRange && playerInSightRange) AttackPlayer(); // If the player is in attack range and sight range, attack the player

        if (invokeDamage)
        {
            TakeDamage(damageAmount);
            invokeDamage = false;
        }
    }

    private void Awake()
    {
        player = GameObject.Find("Player").transform; // Find the player object in the scene
        navMeshAgent = GetComponent<NavMeshAgent>(); // Get the NavMeshAgent component
        currentHealth = maximumHealth;
    }

    private void Patroling()
    {
        if (!walkPointSet) SearchWalkPoint(); // If the patrol point is not set, search for a new one
            
            if (walkPointSet) navMeshAgent.SetDestination(walkPoint); // Set the destination to the patrol point
    
            Vector3 distanceToWalkPoint = transform.position - walkPoint; // Calculate the distance to the patrol point
    
            // If the enemy has reached the patrol point, reset it
            if (distanceToWalkPoint.magnitude < 1f)
            {
                walkPointSet = false;
            }
    }

    private void SearchWalkPoint()
    {
        // Calculate a random patrol point within the specified range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);
        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ); // Set the patrol point

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround)) // Check if the patrol point is on the ground
        {
            walkPointSet = true; // Set the patrol point as valid
        } 
    }

    private void ChasePlayer()
    {
        navMeshAgent.SetDestination(player.position); // Set the destination to the player's position
    }

    private void AttackPlayer()
    {
        Vector3 lookTargetPos = new Vector3(player.position.x, this.transform.position.y, player.position.z);
        navMeshAgent.SetDestination(transform.position); // Stop moving towards the player
        transform.LookAt(lookTargetPos); // Look at the player

        if(!alreadyAttacked)// Attack(); // If the enemy hasn't attacked yet, attack the player
        {
            // Implement attack logic here (e.g., deal damage to the player)
            Debug.Log("Attacking player for " + attackDamage + " damage!");
            alreadyAttacked = true; // Set the attack flag to true
            Invoke(nameof(ResetAttack), timeBetweenAttacks); // Reset the attack flag after the specified time
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false; // Reset the attack flag
    }
    


    // private void MoveTowardsPlayer()
    // {
    //     if (player != null)
    //     {
    //         Vector3 direction = (player.position - transform.position).normalized;
    //         transform.position += direction * speed * Time.deltaTime;
    //     }
    // }

    // private void AttackPlayer()
    // {
    //     if (player != null && Vector3.Distance(transform.position, player.position) <= attackRange)
    //     {
    //         // Implement attack logic here (e.g., deal damage to the player)
    //         Debug.Log("Attacking player for " + attackDamage + " damage!");
    //     }
    // }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        UpdateHealthbar();
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void UpdateHealthbar()
    {
        healthBar.fillAmount = (currentHealth / maximumHealth);
    }

    private void Die()
    {
        Destroy(gameObject); // Destroy the enemy object
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange); // Draw attack range in the editor
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange); // Draw sight range in the editor
    }    
}
