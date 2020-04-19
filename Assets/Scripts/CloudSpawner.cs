using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudSpawner : MonoBehaviour
{
    public GameObject cloudPrefab;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnCloud", 1f, 13f);
    }

    // Update is called once per frame
    void Update()
    {
        GameObject[] clouds = GameObject.FindGameObjectsWithTag("Cloud");
        foreach (GameObject cloud in clouds)
        {
            Vector3 pos = cloud.transform.position;
            if (pos.x >= 3300f)
            {
                Destroy(cloud);
            }
            else
            {
                cloud.transform.position = new Vector3(pos.x + 1f, pos.y, pos.z);
            }
        }
    }

    void SpawnCloud()
    {
        float newY = Random.Range(400, 800);
        Vector3 newPos = new Vector3(transform.position.x - 100f, newY, transform.position.z);
            // get ar random number, and if its less than 70, 
        GameObject newCloud = Instantiate(cloudPrefab, newPos +
    transform.forward, transform.rotation) as GameObject;
        newCloud.transform.SetParent(GameObject.FindGameObjectWithTag("CloudParent").transform);


    }
}
