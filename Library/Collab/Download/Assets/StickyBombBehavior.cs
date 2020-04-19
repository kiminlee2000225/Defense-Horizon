using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickyBombBehavior : MonoBehaviour
{
    public int damageRadius = 5;
    public GameObject glueGlob;
    // public GameObject animation;

    private void OnTriggerEnter(Collider other) {
        // GameObject explosion = Instantiate(animation, transform.position, Quaternion.identity);
        int numGlue = Random.Range(70, 80);
        for (int i = 0; i < numGlue; i++) {
            GameObject glob = Instantiate(glueGlob, transform.position + new Vector3(0, 3) + new Vector3(Random.Range(-2f, 2f), Random.Range(-2f, 2f), Random.Range(-2f, 2f)), Quaternion.identity);
            glob.GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(-5f, 5f), Random.Range(-5f, 5f), Random.Range(-5f, 5f)), ForceMode.Impulse);
            Destroy(glob, Random.Range(10f, 15f));
        }
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, damageRadius);
        print(hitColliders.Length);
        for (int i = 0; i < hitColliders.Length; i++) {
            if (hitColliders[i].CompareTag("Enemy") || hitColliders[i].CompareTag("EnemyBig")) {
                print("Enemy " + i);
                hitColliders[i].gameObject.GetComponent<EnemyHealth>().TakeDamage((int)(50f - (Vector3.Distance(hitColliders[i].transform.position, transform.position) * (50 / damageRadius))));
            }
        }
        Destroy(gameObject);
        
    }
}
