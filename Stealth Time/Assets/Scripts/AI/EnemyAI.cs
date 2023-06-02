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
    }

    void Update()
    {
        if (isFollowingPlayer)
        {
            // Follow the player if within detection range
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);
            if (distanceToPlayer <= detectionRange)
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

            // Check if player is close enough to start following
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);
            if (distanceToPlayer <= detectionRange)
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
