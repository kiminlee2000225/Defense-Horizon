using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public enum FSMStates
    {
        Idle,
        Walk,
        Attack,
        Dead
    }

    public FSMStates currentState;
    public GameObject player;
    public float attackDistance = 2;

    Animator anim;
    NavMeshAgent agent;
    Vector3 targetWallPosition;
    EnemyBehavior enemyBehavior;

    float distanceToWall;
    bool deadAndFacedPlayer;

    public bool canAttack;

    // Start is called before the first frame update
    void Start()
    {
        deadAndFacedPlayer = false;
        canAttack = false;
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        currentState = FSMStates.Walk;
        agent = GetComponent<NavMeshAgent>();
        enemyBehavior = GetComponent<EnemyBehavior>();
    }

    // Update is called once per frame
    void Update()
    {
        targetWallPosition = enemyBehavior.targetWallPosition;
        distanceToWall = Vector3.Distance(transform.position,targetWallPosition);

        switch (currentState)
        {
            case FSMStates.Walk:
                UpdateWalkState();
                break;
            case FSMStates.Attack:
                UpdateAttackState();
                break;
            case FSMStates.Dead:
                UpdateDeadState();
                break;
        }

        if (enemyBehavior.isDead)
        {
            currentState = FSMStates.Dead;
        }
    }

    void UpdateWalkState()
    {
        FaceTarget(agent.destination);
        anim.SetInteger("animState", 1);
        if (distanceToWall <= attackDistance)
        {
            currentState = FSMStates.Attack;
        }
    }

    void UpdateAttackState()
    {
        FaceTarget(agent.destination);
        anim.SetInteger("animState", 2);
        canAttack = true;
        agent.stoppingDistance = attackDistance;
        if (distanceToWall <= attackDistance)
        {
            currentState = FSMStates.Attack;
        }
        else 
        {
            currentState = FSMStates.Walk;
        }
    }

    void UpdateDeadState()
    {
        if (!deadAndFacedPlayer)
        {
            FaceTarget(player.transform.position);
            deadAndFacedPlayer = true;
        }
        canAttack = false;
        anim.SetInteger("animState", 3);
        Destroy(gameObject, 3);
    }

    void FaceTarget(Vector3 target)
    {
        Vector3 directionToTarget = (target - transform.position).normalized;
        directionToTarget.y = 0;
        Quaternion lookRotation = transform.rotation;
        if (directionToTarget != Vector3.zero)
        {
            lookRotation = Quaternion.LookRotation(directionToTarget);
        } 
        transform.rotation = lookRotation;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackDistance);
    }
}
