using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLook : MonoBehaviour
{

    [SerializeField] public int mouseSensitivity = 5;
    public Transform playerCamera;
    float xRotation, yRotation;
    private float mouseX, mouseY;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void OnLook(InputValue input)
    {
        mouseX = input.Get<Vector2>().x;
        mouseY = input.Get<Vector2>().y;
    }

    // Update is called once per frame
    void Update()
    {
        // Scaling mouse movement by deltaTime and sensitivity
        mouseX *= Time.deltaTime * mouseSensitivity;
        mouseY *= Time.deltaTime * mouseSensitivity;

        // Adjusting yRotation based on mouseX movement
        yRotation += mouseX;

        // Adjusting xRotation based on mouseY movement, and clamping it within a range
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -85f, 85f);

        // Applying rotation to the player object (for left and right rotation)
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);

        // Applying rotation to the player's camera (Both vertical and horizontal rotation)
        playerCamera.rotation = Quaternion.Euler(xRotation, yRotation, 0);

    }
}
