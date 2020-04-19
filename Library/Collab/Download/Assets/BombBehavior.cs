using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombBehavior : MonoBehaviour
{
    public int damageRadius = 8;
    public GameObject animation;

    private void OnTriggerEnter(Collider other) {
        GameObject explosion = Instantiate(animation, transform.position, Quaternion.identity);
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, damageRadius);
        print(hitColliders.Length);
        for (int i = 0; i < hitColliders.Length; i++) {
            if (hitColliders[i].CompareTag("Enemy") || hitColliders[i].CompareTag("EnemyBig")) {
                print("Enemy " + i);
                hitColliders[i].gameObject.GetComponent<EnemyHealth>().TakeDamage((int)(150f - (Vector3.Distance(hitColliders[i].transform.position, transform.position) * (100 / damageRadius - 3))));
            }
        }
        Destroy(gameObject, 0.05f);
        Destroy(explosion, 2.5f);
        
    }
}
