using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Rigidbody rb = gameObject.GetComponent<Rigidbody>();
        rb.velocity = new Vector3(0, -9, 0);
        
    }

    // Update is called once per frame
    void Update()
    {
        Rigidbody rb = gameObject.GetComponent<Rigidbody>();
        if (rb.velocity.y > -8) {
            rb.useGravity = false;
            rb.velocity = new Vector3(0, 0, 0);
        }
        
    }

    private void OnTriggerEnter(Collider collider) {
        if (collider.CompareTag("Enemy") || collider.CompareTag("EnemyBig")) {
            collider.gameObject.GetComponent<EnemyBehavior>().ApplySlow();
        }
    }
}
