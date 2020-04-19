using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopBehavior : MonoBehaviour
{
    public GameObject player;
    public GameObject menu; 
    public int distanceToAccess = 3;
    public Text shopText;

    GameObject playerCamera;
    public static bool shopOpen = false;
    // Start is called before the first frame update
    void Start()
    {
        shopOpen = false;
        menu.SetActive(false);
        playerCamera = GameObject.FindWithTag("MainCamera");
        shopText.text = "Press E to open/close shop!";
    }

    // Update is called once per frame
    void Update()
    {
        if (closeToPlayer())
        {
            shopText.gameObject.SetActive(true);
        } else
        {
            shopText.gameObject.SetActive(false);
        }

        if (!shopOpen && closeToPlayer() && Input.GetKey(KeyCode.E)) {
            OpenShop();
        }
        else if (shopOpen && Input.GetKeyDown(KeyCode.Escape)) {
            CloseShop();
        }
        
    }

    public void OpenShop() {
        shopOpen = true;
        Time.timeScale = 0f;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        menu.SetActive(true);
    }

    public void CloseShop() {
        shopOpen = false;
        Time.timeScale = 1f;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        menu.SetActive(false);
    }

    bool closeToPlayer()
    {
        return Vector3.Distance(transform.position, player.transform.position) <= distanceToAccess;
    }
}
