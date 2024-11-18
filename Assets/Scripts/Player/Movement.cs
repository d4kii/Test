using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public CharacterController playerController;

    public float playerSpeed = 12f;
    public float dashSpeed = 500f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;

    public Transform groundCheckTransform;
    public float groundCheckRadius = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;
    bool isPlayerGrounded;

    // Update is called once per frame
    void Update()
    {
        // Inputs
        if (Input.GetButtonDown("Jump") && isPlayerGrounded) Jump();
        if (Input.GetButtonDown("Dash") && isPlayerGrounded) Dash();

        playerController.Move(MoveDirection() * playerSpeed * Time.deltaTime); // Moves the player, dicated by the player inputs.

        Gravity();
    }

    /// <summary>
    /// Applies gravity. !!!ATTENTION!!! ALSO UPDATES THE isPlayerGrounded bool!!! change it!!
    /// </summary>
    void Gravity()
    {
        // Is player on the ground? Checks the default tag, player stuff is in the Player tag.
        isPlayerGrounded = Physics.CheckSphere(groundCheckTransform.position, groundCheckRadius, groundMask);

        if (isPlayerGrounded && velocity.y < 0) velocity.y = gravity; // "Default" gravity to keep the player on the ground!
        else velocity.y += gravity * Time.deltaTime; // Adds the gravity over time.

        playerController.Move(velocity * Time.deltaTime); // Moves the player according to the gravity.
    }

    /// <summary>
    /// Returns the direction the player wants to move in based on inputs.
    /// </summary>
    Vector3 MoveDirection() {
        // Get's the inputs.
        float xInput = Input.GetAxisRaw("Horizontal");
        float zInput = Input.GetAxisRaw("Vertical");
        
        Vector3 moveDirection = Camera.main.transform.forward * zInput + Camera.main.transform.right * xInput; // ??? Magic math ???
        moveDirection.y = 0f; // ???????? WHY DO I HAVE TO DO THIS, THE PLAYER FLIES IF I DON'T
        moveDirection = Vector3.Normalize(moveDirection); // Normalizes between 0 and 1

        return moveDirection;
    }

    /// <summary>
    /// Jumps.
    /// </summary>
    void Jump()
    {
        velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity); // Thrusts the player upwards when space is pressed.
    }

    /// <summary>
    /// Dashes.
    /// </summary>
    void Dash()
    {
        playerController.Move(MoveDirection() * dashSpeed * Time.deltaTime);
    }
}
