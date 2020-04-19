using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WallBehavior : MonoBehaviour
{
    public int maxHealth;
    public int damageAmount;
    public int damageAmountBig;
    public int secondsPerHit;
    public GameObject gs;
    public int bounceConstant;
    public int health;
    private float lastHitTime;
    public bool change;
    private Color startColor;
    public Text healthView;
    private Vector3 cameraPosition;
    public AudioClip turretBreaksSFX;
    public AudioClip turretHitSFX;
    private bool broken;

    // Start is called before the first frame update
    void Start()
    {
        broken = false;
        health = maxHealth;
        change = false;
        startColor = gameObject.GetComponent<Renderer>().material.color;
    }

    // Update is called once per frame
    void Update()
    {
        cameraPosition = GameObject.FindGameObjectWithTag("MainCamera").transform.position;
        var cren = gameObject.transform.GetChild(0);
        var main = gameObject.transform.GetChild(1);
        var wallTop = gameObject.transform.GetChild(2);

        if (change)
        {
            Material m = new Material(Shader.Find("Diffuse"));
            m.color = Color.yellow;
            main.GetComponent<MeshRenderer>().materials[0] = m;
            main.GetComponent<MeshRenderer>().materials[1] = m;
            main.GetComponent<MeshRenderer>().materials[2] = m;
            main.GetComponent<MeshRenderer>().materials[3] = m;
            main.GetComponent<MeshRenderer>().materials[4] = m;
            cren.GetComponent<Renderer>().material.color = Color.yellow;
            main.GetComponent<Renderer>().material.color = Color.yellow;
            wallTop.GetComponent<Renderer>().material.color = Color.yellow;
            gameObject.GetComponent<Renderer>().material.color = Color.yellow;
            healthView.gameObject.SetActive(true);
        } else
        {
            Material m = new Material(Shader.Find("Diffuse"));
            m.color = startColor;
            main.GetComponent<MeshRenderer>().materials[0] = m;
            main.GetComponent<MeshRenderer>().materials[1] = m;
            main.GetComponent<MeshRenderer>().materials[2] = m;
            main.GetComponent<MeshRenderer>().materials[3] = m;
            main.GetComponent<MeshRenderer>().materials[4] = m;
            cren.GetComponent<Renderer>().material.color = startColor;
            main.GetComponent<Renderer>().material.color = startColor;
            wallTop.GetComponent<Renderer>().material.color = startColor;
            gameObject.GetComponent<Renderer>().material.color = startColor;
            healthView.gameObject.SetActive(false);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Enemy") && (Time.time - lastHitTime) > secondsPerHit)
        {
            if (other.gameObject.GetComponent<EnemyAI>().canAttack)
            {
                takeDamage("small");
                lastHitTime = Time.time;
            }
        } else if (other.CompareTag("EnemyBig") && (Time.time - lastHitTime) > secondsPerHit)
        {
            if (other.gameObject.GetComponent<EnemyAI>().canAttack)
            {
                takeDamage("big");
                lastHitTime = Time.time;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") && (Time.time - lastHitTime) > secondsPerHit) {
            if (other.gameObject.GetComponent<EnemyAI>().canAttack)
            {
                takeDamage("small");
                lastHitTime = Time.time;
            }
        }
        else if (other.CompareTag("EnemyBig") && (Time.time - lastHitTime) > secondsPerHit)
        {
            if (other.gameObject.GetComponent<EnemyAI>().canAttack)
            {
                takeDamage("big");
                lastHitTime = Time.time;
            }
        }
    }

    private void takeDamage(string smallOrBig)
    {
        gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.Lerp(Color.white, Color.black, health));

        if (!broken)
        {
            AudioSource.PlayClipAtPoint(turretHitSFX, cameraPosition);
            if (smallOrBig == "small")
            {
                if (health - damageAmount <= 0)
                {
                    broken = true;
                    AudioSource.PlayClipAtPoint(turretBreaksSFX, cameraPosition);
                    health = 0;
                    gs.GetComponent<GameState>().GameOver();
                }
                else
                {
                    health -= damageAmount;
                }
            } else
            {
                if (health - damageAmountBig <= 0)
                {
                    broken = true;
                    AudioSource.PlayClipAtPoint(turretBreaksSFX, cameraPosition);
                    health = 0;
                    gs.GetComponent<GameState>().GameOver();
                }
                else
                {
                    health -= damageAmountBig;
                }
            }
        }
    }

    public void UpgradeWalls() {
        maxHealth += 20;
        health += 20;
    }
}
