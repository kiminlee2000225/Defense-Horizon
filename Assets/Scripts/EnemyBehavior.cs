using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehavior : MonoBehaviour
{
    public Transform target;
    public float moveSpeed = 20f;
    public GameState gs;
    private NavMeshAgent agent;
    public bool isDead;
    public Vector3 targetWallPosition;

    void Start()
    {
        isDead = false;
        if (target == null)
        {
            GameObject[] walls = GameObject.FindGameObjectsWithTag("WallPosition");
            target = walls[Random.Range(0, walls.Length)].transform;
            targetWallPosition = target.transform.position;
        }
        if (gs == null)
        {
            gs = GameObject.FindWithTag("GameState").GetComponent<GameState>();
        }
        agent = GetComponent<NavMeshAgent>();
        RaycastHit hit;
        if (Physics.Raycast(new Ray(transform.position, Vector3.down), out hit))
        {
            Vector3 newPos = new Vector3(transform.position.x, transform.position.y - hit.distance + 1, transform.position.z);
            agent.Warp(newPos);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead)
        {
            agent.SetDestination(transform.position);
        } else
        {
            agent.SetDestination(targetWallPosition);
        }
    }

    public void ApplyPoison() {
        InvokeRepeating("TakePoisonDamage", 1f, 1f);
    }

    void TakePoisonDamage() {
        
    }

    public void ApplySlow() {
        agent.speed = 2;
        Invoke("RevertSlow", 10.0f);
    }

    void RevertSlow() {
        agent.speed = 8;
    }

}
