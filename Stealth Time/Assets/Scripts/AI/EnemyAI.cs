using StarterAssets;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

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

    public float takedownDistance = 2f;
    public float destroyDelay = 2f;

    private ThirdPersonController playerController; // Reference to the ThirdPersonController script
    private Dissolve PlayerInvis; // reference if dissolve script

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
        PlayerInvis = player.GetComponent<Dissolve>(); //Get the Dissolve script from the player
    }

    void Update()
    {
        if (isFollowingPlayer)
        {
            // Follow the player if within detection range and not crouched
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);
            bool isPlayerCrouched = playerController.isCrouched; // Get the isCrouched property from the ThirdPersonController script
            bool isPlayerInvis = PlayerInvis.isInvis;

            if (distanceToPlayer <= detectionRange && isPlayerInvis == false)
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

            // Check if the enemy is within a certain distance of the player
            if (distanceToPlayer <= takedownDistance)
            {
                EndGame();
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
            bool isPlayerInvis = PlayerInvis.isInvis;

            if (distanceToPlayer <= detectionRange && isPlayerInvis == false)
            {
                isFollowingPlayer = true;
                isPatrolling = false;
                SetDestination(player);
                navAgent.speed = followSpeed;
            }
        }
    }

    void EndGame()
    {
        // Change to the next scene or perform game over logic here
        SceneManager.LoadScene("Mission Failed");
    }

    void CheckTakedownInput()
    {
        bool isPlayerCrouched = playerController.isCrouched; // Get the isCrouched property from the ThirdPersonController script

        // Check if player is crouched and press Key T
        if (Input.GetKeyDown(KeyCode.T) && isPlayerCrouched)
        {
            Vector3 playerDirection = (player.position - transform.position).normalized;
            Vector3 enemyDirection = transform.forward;
            float angle = Vector3.Angle(playerDirection, enemyDirection);

            // Check if player is behind the AI and within takedown distance
            if (angle < 270 && Vector3.Distance(transform.position, player.position) <= takedownDistance)
            {
                // Play death animation
                Animator animator = GetComponent<Animator>();
                animator.SetBool("IsDead", true);

                // Disable enemy movement
                navAgent.enabled = false;

                // Destroy the enemy AI after the specified delay
                Destroy(gameObject, destroyDelay);
            }
        }
    }

    void SetDestination(Transform target)
    {
        navAgent.SetDestination(target.position);
    }
}
