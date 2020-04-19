using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealTurret : MonoBehaviour
{
    WallBehavior wb;
    GameObject player;
    float distanceToPlayer;
    float timer = 0f;
    float startTime = 0f;
    bool held = false;
    bool heldForHeal = false;

    Vector3 cameraPosition;

    public int healAmount = 1;
    public float holdTime = 0.1f;
    public string key = "r";
    public AudioClip buildSFX;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0f;
        startTime = 0f;
        wb = GetComponent<WallBehavior>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        cameraPosition = GameObject.FindGameObjectWithTag("MainCamera").transform.position;
        timer += Time.deltaTime;

        distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        if (wb.health < 100)
        {
            if (wb.change && distanceToPlayer <= 11f)
            {
                if (heldForHeal)
                {
                    Heal();
                    heldForHeal = false;
                    startTime += Time.time;
                    timer = startTime;
                    AudioSource.PlayClipAtPoint(buildSFX, cameraPosition);
                }

                if (Input.GetKeyUp(key))
                {
                    held = false;
                    heldForHeal = false;
                    //startTime = timer + Time.time;
                    //timer = startTime;
                }

                if (Input.GetKeyDown(key))
                {
                    startTime += Time.time;
                    timer = startTime;
                    held = true;
                    AudioSource.PlayClipAtPoint(buildSFX, cameraPosition);
                }

                if (Input.GetKey(key) && held == true)
                {
                    if (timer > (startTime + holdTime))
                    {
                        heldForHeal = true;
                    }
                }
            }
        }

    }

    void Heal()
    {
        if (wb.health <= 100)
        {
            if (wb.health + healAmount > 100)
            {
                wb.health = 100;
            } else
            {
                wb.health += healAmount;
            }
        }
    }
}
