using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerBehavior : MonoBehaviour
{
    public int spawnRate;
    public int spawnRateBig;
    public int spawnBigMore;
    public GameObject prefab;
    public GameObject prefabBig;

    // Start is called before the first frame update
    void Start()
    {
        spawnRate = 30;
        spawnRateBig = 95;
        spawnBigMore = 90;
        if (GameState.currLevel == 1)
        {
            InvokeRepeating("Spawn", 1, 1.5f);
        } else if (GameState.currLevel == 2)
        {
            InvokeRepeating("SpawnBig", 1, 2);
        } else
        {
            InvokeRepeating("SpawnBigMore", 1, 2);
        }
    }

    void Spawn()
    {
        int rand = Random.Range(0, 100);
        if (rand < spawnRate)
        {
            Vector3 position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z + Random.Range(-90, 90));
            GameObject newswordsmen = Instantiate(prefab, position, Quaternion.identity);
            newswordsmen.transform.SetParent(GameObject.FindGameObjectWithTag("Spawner").transform);
        }
    }

    void SpawnBig()
    {
        int rand = Random.Range(0, 100);
        if (rand < spawnRate)
        {
            Vector3 position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z + Random.Range(-90, 90));
            GameObject newswordsmen = Instantiate(prefab, position, Quaternion.identity);
            newswordsmen.transform.SetParent(GameObject.FindGameObjectWithTag("Spawner").transform);
        }
        if (rand >= spawnRateBig)
        {
            Vector3 position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z + Random.Range(-90, 90));
            GameObject newKnight = Instantiate(prefabBig, position, Quaternion.identity);
            newKnight.transform.SetParent(GameObject.FindGameObjectWithTag("Spawner").transform);
        }
    }

    void SpawnBigMore()
    {
        int rand = Random.Range(0, 100);
        if (rand < spawnRate)
        {
            Vector3 position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z + Random.Range(-90, 90));
            GameObject newswordsmen = Instantiate(prefab, position, Quaternion.identity);
            newswordsmen.transform.SetParent(GameObject.FindGameObjectWithTag("Spawner").transform);
        }
        if (rand >= spawnBigMore)
        {
            Vector3 position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z + Random.Range(-90, 90));
            GameObject newKnight = Instantiate(prefabBig, position, Quaternion.identity);
            newKnight.transform.SetParent(GameObject.FindGameObjectWithTag("Spawner").transform);
        }
    }
}
