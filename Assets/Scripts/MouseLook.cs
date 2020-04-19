using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{

    public Transform playerBody;
    public float mouseSensitivity = 10f;
    float xRotation = 0;
    // Start is called before the first frame update
    void Start()
    {
        if (playerBody == null)
        {
            playerBody = transform.parent.transform;
        }

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if(!PauseMenuBehaviour.isGamePaused)
        {
            float moveX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float moveY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            // yaw applied to Capsule (parent)
            playerBody.Rotate(Vector3.up * moveX);

            xRotation -= moveY;

            xRotation = Mathf.Clamp(xRotation, -90.0f, 90.0f);

            // pitch applie to Camera (child)
            transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        }
        


    }
}
