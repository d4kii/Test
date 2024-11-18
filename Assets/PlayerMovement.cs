using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController playerController;

    public float playerSpeed = 12f;
    

    float xInput;
    float zInput;

    Vector3 moveDirection;

    // Update is called once per frame
    void Update()
    {
        xInput = Input.GetAxisRaw("Horizontal");
        zInput = Input.GetAxisRaw("Vertical");

        moveDirection = Camera.main.transform.forward * zInput + Camera.main.transform.right * xInput;
        moveDirection.y = 0f;
        moveDirection = Vector3.Normalize(moveDirection);
        playerController.Move(moveDirection * playerSpeed * Time.deltaTime);
    }
}
