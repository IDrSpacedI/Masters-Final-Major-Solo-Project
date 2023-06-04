using StarterAssets;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public Transform player;
    public float detectionRange = 10f;
    public float followSpeed = 3f;
    public float patrolSpeed = 1.5f;
    public Transform[] waypoints;

    private NavMeshAgent navAgent;
    private Transform currentTarget;
    private int currentWaypointIndex;
    private bool isFollowingPlayer = false;
    private bool isPatrolling = true;

    private ThirdPersonController playerController; // Reference to the ThirdPersonController script

    void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();

        if (waypoints.Length > 0)
        {
            // Start by patrolling to the first waypoint
            currentWaypointIndex = 0;
            currentTarget = waypoints[currentWaypointIndex];
            SetDestination(currentTarget);
            navAgent.speed = patrolSpeed; // Set the initial patrol speed
        }
        else
        {
            Debug.LogError("No waypoints assigned to the enemy!");
        }

        playerController = player.GetComponent<ThirdPersonController>(); // Get the ThirdPersonController script from the player
    }

    void Update()
    {
        if (isFollowingPlayer)
        {
            // Follow the player if within detection range and not crouched
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);
            bool isPlayerCrouched = playerController.isCrouched; // Get the isCrouched property from the ThirdPersonController script

            if (distanceToPlayer <= detectionRange && !isPlayerCrouched)
            {
                SetDestination(player);
                navAgent.speed = followSpeed;
            }
            else
            {
                isFollowingPlayer = false;
                isPatrolling = true;
                SetDestination(currentTarget);
                navAgent.speed = patrolSpeed;
            }
        }
        else if (isPatrolling)
        {
            // Patrol between waypoints
            if (navAgent.remainingDistance <= navAgent.stoppingDistance)
            {
                currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
                currentTarget = waypoints[currentWaypointIndex];
                SetDestination(currentTarget);
            }

            // Check if player is close enough and not crouched to start following
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);
            bool isPlayerCrouched = playerController.isCrouched; // Get the isCrouched property from the ThirdPersonController script

            if (distanceToPlayer <= detectionRange && !isPlayerCrouched)
            {
                isFollowingPlayer = true;
                isPatrolling = false;
                SetDestination(player);
                navAgent.speed = followSpeed;
            }
        }
    }

    void SetDestination(Transform target)
    {
        navAgent.SetDestination(target.position);
    }
}
