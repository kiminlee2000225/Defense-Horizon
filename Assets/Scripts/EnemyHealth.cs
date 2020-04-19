using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public  int currentHealth;
    public AudioClip deadSFX;
    public Slider healthSlider;
    public int value = 5;
    EnemyBehavior enemyBehavior;
    Vector3 cameraPosition;
    GameObject player;

    private void Awake()
    {
        healthSlider = GetComponentInChildren<Slider>();
        currentHealth = startingHealth;
    }
    // Start is called before the first frame update
    void Start()
    {

        enemyBehavior = gameObject.GetComponent<EnemyBehavior>();
        currentHealth = startingHealth;
        healthSlider.value = currentHealth;
        player = GameObject.FindWithTag("Player");
    }

    private void Update()
    {
        cameraPosition = GameObject.FindGameObjectWithTag("MainCamera").transform.position;
    }

    public void TakeDamageArrow(ArrowStats arrow)
    {
        if (arrow.poison) {
            InvokeRepeating("TakePoisonDamage", 1f, 1f);
        }

        if (arrow.ice) {
            enemyBehavior.ApplySlow();
        }

        TakeDamage(arrow.damage);
    }

    public void TakePoisonDamage() {
        TakeDamage(5);
    }

    public void TakeDamage(int damage) {
        if (currentHealth > 0)
        {
            currentHealth -= damage;
            healthSlider.value = currentHealth;
        }

        if (currentHealth <= 0)
        {
            //dead
            if (!enemyBehavior.isDead)
            {
                player.GetComponent<Inventory>().addGold(value);
            }
            gameObject.GetComponent<CapsuleCollider>().enabled = false;
            AudioSource.PlayClipAtPoint(deadSFX, cameraPosition);
            enemyBehavior.isDead = true;
            ++GameState.enemyKilled;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            TakeDamageArrow(other.gameObject.GetComponent<ArrowStats>());
            Destroy(other.gameObject);

        }
    }
}
