using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public Transform playerTransform;

    public float mouseSensitivity = 100f;

    float mouseX;
    float mouseY;

    float xRotation = 0f;
    float yRotation = 0f;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * mouseSensitivity;
        mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * mouseSensitivity;

        yRotation += mouseX;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -89.99f, 89.99f);

        transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);
        playerTransform.rotation = Quaternion.Euler(0f, yRotation, 0f);
    }
}
