using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    private Rigidbody rb;
    public GameState gs;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!GameState.isGameOver)
        {
            if (other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("EnemyBig"))
            {
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
                gs.GetComponent<GameState>().GameOver();
            }
        }
    }
}
