using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    CharacterController controller;
    public float moveSpeed = 5f;
    public float gravity = 9.81f;
    Vector3 moveDirection;
    public float jumpHeight = 3f;
    public float airControl = 10f;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        var input = transform.right * moveHorizontal + transform.forward * moveVertical;

        input *= moveSpeed;

        if (controller.isGrounded)
        {
            moveDirection = input;
            if (Input.GetButton("Jump"))
            {
                moveDirection.y = Mathf.Sqrt(2 * gravity * jumpHeight);
            }
            else
            {
                moveDirection.y = 0f;
            }
        }
        else
        {
            input.y = moveDirection.y;
            moveDirection = Vector3.Lerp(moveDirection, input, airControl * Time.deltaTime);

        }

        moveDirection.y -= gravity * Time.deltaTime;

        controller.Move(moveDirection * Time.deltaTime);
    }

    public void playerIsDead()
    {
        controller.enabled = false;
        gameObject.GetComponent<CapsuleCollider>().enabled = true;
        gameObject.GetComponent<Rigidbody>().isKinematic = true;
        gameObject.GetComponent<Rigidbody>().detectCollisions = false;
    }

    public void playerWon()
    {
        controller.enabled = false;
        gameObject.GetComponent<CapsuleCollider>().enabled = true;
        gameObject.GetComponent<Rigidbody>().isKinematic = true;
        gameObject.GetComponent<Rigidbody>().detectCollisions = false;
    }
}
