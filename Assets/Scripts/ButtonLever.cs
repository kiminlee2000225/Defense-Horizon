using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonLever : MonoBehaviour
{
    // NearView()
    Animator anim;
    Vector3 cameraPosition;

    public static bool isOpened;

    public GameObject player;
    public GameObject grid;
    public GameObject ramp;
    public AudioClip doorSFX;

    bool active = true;

    public Text openText;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        isOpened = false;
        player = GameObject.FindGameObjectWithTag("Player");
        openText.text = "Press E to open/close door!";
    }

    // Update is called once per frame
    void Update()
    {
        cameraPosition = GameObject.FindGameObjectWithTag("MainCamera").transform.position;
        if (closeToPlayer())
        {
            openText.gameObject.SetActive(true);
        } else
        {
            openText.gameObject.SetActive(false);
        }

        if (active && Input.GetKey(KeyCode.E) && closeToPlayer())
        {
            active = false;
            Invoke("reactivate", 1f);
            //print("pressed");
            if (isOpened)
            {
                anim.SetBool("LeverUp", true);
                grid.transform.position = new Vector3(grid.transform.position.x, grid.transform.position.y - 3.3f, grid.transform.position.z);
                ramp.transform.Rotate(0f, 0f, -3f);
                ramp.transform.Rotate(-84, 0f, 0f);
 
                ramp.transform.position = new Vector3(ramp.transform.position.x, ramp.transform.position.y - 0.42f, ramp.transform.position.z);
                isOpened = !isOpened;
            }
            else
            {
                anim.SetBool("LeverUp", false);
                grid.transform.position = new Vector3(grid.transform.position.x, grid.transform.position.y + 3.3f, grid.transform.position.z);
                ramp.transform.Rotate(84, 0f, 0f);
                ramp.transform.Rotate(0f, 0f, 3f);
                ramp.transform.position = new Vector3(ramp.transform.position.x, ramp.transform.position.y + 0.42f, ramp.transform.position.z);
                isOpened = !isOpened;
            }

            AudioSource.PlayClipAtPoint(doorSFX, cameraPosition);
        } 
    }

    private void reactivate() {
        active = true;
    }

    bool closeToPlayer()
    {
        return Vector3.Distance(transform.position, player.transform.position) <= 3f;
    }
}
